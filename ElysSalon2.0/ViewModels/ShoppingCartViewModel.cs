﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Application.DTOs.Request.SalesData;
using Application.DTOs.Request.Tickets;
using Application.DTOs.Request.TicketsDetails;
using Application.DTOs.Response.Articles;
using Application.Services;
using Application.Utils;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.views;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.ViewModels;

public class ShoppingCartViewModel : INotifyPropertyChanged
{
    #region Fields

    private readonly ArticleAppService _articleService;
    private readonly IMapper _mapper;
    private readonly Window _window;
    private readonly WindowsManager _windowsManager;
    private readonly SaleDataAppService _saleDataService;

    private ObservableCollection<DtoCreateTicketDetails> _cartItems;
    private Window _confirmWindow;
    private string _issuer = "messi";
    private decimal _totalAmount;

    public ICollectionView CartItemsView { get; private set; }

    #endregion

    #region Constructor

    public ShoppingCartViewModel(
        ArticleAppService articleService,
        IMapper mapper,
        SaleDataAppService saleDataService,
        WindowsManager windowsManager,
        Window window)
    {
        _articleService = articleService;
        _mapper = mapper;
        _saleDataService = saleDataService;
        _window = window;
        _windowsManager = windowsManager;

        InitializeCollections();
        InitializeCommands();
        InitializeArticleButtons();
    }

    private void InitializeCollections()
    {
        _cartItems = new ObservableCollection<DtoCreateTicketDetails>();
        CartItemsView = CollectionViewSource.GetDefaultView(_cartItems);
        ArticlesButtons = new ObservableCollection<Button>();
    }

    private void InitializeCommands()
    {
        RemoveFromCartCommand = new RelayCommand<DtoCreateTicketDetails>(RemoveFromCart);
        DecreaseQuantityCommand = new RelayCommand<DtoCreateTicketDetails>(DecreaseQuantity);
        IncreaseQuantityCommand = new RelayCommand<DtoCreateTicketDetails>(IncreaseQuantity);

        CloseShoppingCartCommand = new RelayCommand(CloseShoppingCart);
        OpenConfirmWindowCommand = new RelayCommand(OpenConfirmWindow);
        CloseConfirmWindowCommand = new RelayCommand(CloseConfirmWindow);
        SaveTicketsDetailsCommand = new AsyncRelayCommand(SaveTicketDetails);
    }

    private async void InitializeArticleButtons()
    {
        try
        {
            await LoadArticlesButtons();
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
        }
    }

    #endregion

    #region Properties

    public ObservableCollection<Button> ArticlesButtons { get; private set; }

    public decimal TotalAmount
    {
        get => _totalAmount;
        set => SetField(ref _totalAmount, value);
    }

    public ObservableCollection<DtoCreateTicketDetails> CartItems
    {
        get => _cartItems;
        set => SetField(ref _cartItems, value);
    }

    public string Issuer
    {
        get => _issuer;
        set => SetField(ref _issuer, value);
    }

    #endregion

    #region Commands

    public ICommand OpenConfirmWindowCommand { get; private set; }
    public ICommand SaveTicketsDetailsCommand { get; private set; }
    public ICommand CloseConfirmWindowCommand { get; private set; }
    public ICommand CloseShoppingCartCommand { get; private set; }
    public ICommand RemoveFromCartCommand { get; private set; }
    public ICommand IncreaseQuantityCommand { get; private set; }
    public ICommand DecreaseQuantityCommand { get; private set; }

    #endregion

    #region Private Methods

    private async Task LoadArticlesButtons()
    {
        var articles = (ObservableCollection<DTOGetArticlesButton>)(await _articleService.GetArticlesToButtons()).Data;

        if (articles == null) return;

        foreach (var article in articles)
        {
            var button = CreateArticleButton(article);
            ArticlesButtons.Add(button);
        }
    }

    private Button CreateArticleButton(DTOGetArticlesButton article)
    {
        //    MessageBox.Show($"Creating button for article: {article.Article.Name}");
        var button = new Button
        {
            Tag = article.Article.ArticleId,
            Style = (Style)App.Current.FindResource("articlesBtn"),
            Padding = new Thickness(10),
            Content = new TextBlock
            {
                Text = article.Article.Name,
                TextWrapping = TextWrapping.Wrap,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 27
            }
        };

        button.Click += async (sender, e) => await AddToCart(_mapper.Map<DtoCreateTicketDetails>(article));
        return button;
    }

    private async Task AddToCart(DtoCreateTicketDetails article)
    {
        var existingItem = CartItems.FirstOrDefault(x => x.Article.ArticleId == article.Article.ArticleId);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            existingItem = new DtoCreateTicketDetails(null, article.Article, 1, article.TotalPrice);
            _cartItems.Add(existingItem);
        }

        TotalAmount += article.TotalPrice;
        CartItemsView?.Refresh();
    }

    private async Task SaveTicketDetails()
    {
        var ticketResult = await _saleDataService.GetLastIdTicket();
        var ticket = (DTOSalesData)ticketResult.Data;
        var dto = new DtoCreateTicket(IdGeneratorUtil.GenerateNewId(ticket.Id), DateTime.Now, Issuer,
            TotalAmount);

        var saveTicketOperation = await _saleDataService.Add<DtoCreateTicket>(dto);

        if (saveTicketOperation.Success)
        {
            var saveRangeOperation = await _saleDataService.AddTicketDetailsRange(CartItems.ToList());
            if (saveRangeOperation.Success)
            {
                ClearCart();
                _window.Close();
                _windowsManager.CloseCurrentWindowandShowWindow<MainWindow>(_confirmWindow);
            }
        }
    }

    #endregion

    #region UIMethods

    private void OpenConfirmWindow()
    {
        var confirmWindow = new ConfirmWindow(this);
        _confirmWindow = confirmWindow;
        confirmWindow.ShowDialog();
    }

    private void CloseConfirmWindow()
    {
        if (_confirmWindow == null) return;
        _windowsManager.CloseCurrentWindowandShowWindow<ShoppingCartWindow>(_confirmWindow);
    }

    private void CloseShoppingCart()
    {
        _windowsManager.CloseCurrentWindowandShowWindow<MainWindow>(_window);
    }

    public void RemoveFromCart(DtoCreateTicketDetails ticket)
    {
        if (ticket == null) return;

        CartItems.Remove(ticket);
        UpdateTotalPrice();
    }

    private void DecreaseQuantity(DtoCreateTicketDetails ticket)
    {
        if (ticket == null) return;

        if (ticket.Quantity > 1)
            ticket.Quantity--;
        else
            RemoveFromCart(ticket);

        UpdateTotalPrice();
    }

    private void IncreaseQuantity(DtoCreateTicketDetails ticket)
    {
        if (ticket == null) return;

        ticket.Quantity++;
        UpdateTotalPrice();
    }

    private void UpdateTotalPrice()
    {
        TotalAmount = CartItems.Sum(item => item.TotalPrice);
        CartItemsView.Refresh();
    }

    public void ClearCart()
    {
        CartItems.Clear();
        TotalAmount = 0;
    }

    #endregion

    #region INotifyPropertyChanged Implementation

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    #endregion
}