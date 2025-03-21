using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.InBound.UI.views;
using ElysSalon2._0.adapters.InBound.UI.views.MainViews;
using ElysSalon2._0.Core.aplication.DTOs.DTOArticle;
using ElysSalon2._0.Core.aplication.DTOs.DTOTicket;
using ElysSalon2._0.Core.aplication.Management;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.aplication.Ports.Services;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.adapters.InBound.UI.ViewModels;

public class ShoppingCartViewModel : INotifyPropertyChanged
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly ITicketService _service;
    private readonly Window _window;
    private readonly WindowsManager _windowsManager;
    private ObservableCollection<TicketDetails> _cartItems;
    private Window _confirmWindow;
    private string _issuer = "messi";
    private Ticket _ticket;
    private decimal _totalAmount;
    public ICollectionView cartItemsView;


    public ShoppingCartViewModel(IArticleRepository articleRepository, IMapper mapper, ITicketService service,
        WindowsManager windowsManager, Window window)
    {
        _service = service;
        _mapper = mapper;
        _window = window;
        _windowsManager = windowsManager;
        _cartItems = new ObservableCollection<TicketDetails>();
        cartItemsView = CollectionViewSource.GetDefaultView(_cartItems);
        ArticlesButtons = new ObservableCollection<Button>();
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        loadButtons();


        RemoveFromCartCommand = new RelayCommand<TicketDetails>(RemoveFromCart);
        DecreaseQuantityCommand = new RelayCommand<TicketDetails>(DecreaseFromCart);
        IncreaseQuantityCommand = new RelayCommand<TicketDetails>(IncreaseQuantity);

        CloseShopingCartCommand = new RelayCommand(CloseShopingCart);
        OpenConfirmsWindowCommand = new RelayCommand(OpenConfirmWindow);
        CloseConfirmWindowCommand = new RelayCommand(CloseConfirmWindow);
        SaveTicketsDetailsCommand = new AsyncRelayCommand(SaveTicketDetails);
    }


    public ObservableCollection<Button> ArticlesButtons { get; set; }

    //COMMANDS 
    public ICommand OpenConfirmsWindowCommand { get; }
    public ICommand SaveTicketsDetailsCommand { get; }
    public ICommand CloseConfirmWindowCommand { get; }
    public ICommand CloseShopingCartCommand { get; }
    public ICommand RemoveFromCartCommand { get; }
    public ICommand IncreaseQuantityCommand { get; }
    public ICommand DecreaseQuantityCommand { get; }

    public decimal totalAmount
    {
        get => _totalAmount;
        set
        {
            _totalAmount = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<TicketDetails> cartItems
    {
        get => _cartItems;
        set
        {
            _cartItems = value;
            OnPropertyChanged();
        }
    }

    public string Issuer
    {
        get => _issuer;
        set => SetField(ref _issuer, value);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private async Task loadButtons()
    {
        var articles = await _articleRepository.GetArticlesToButtonAsync();

        if (articles != null && articles.Any())
            foreach (var article in articles)
            {
                var btn = new Button
                {
                    Tag = article.ArticleId,
                    Style = (Style)Application.Current.FindResource("articlesBtn"),
                    Padding = new Thickness(10)
                };
                btn.Click += async (e, s) => { AddToCart(article); };
                var textBlock = new TextBlock
                {
                    Text = article.Name,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    FontSize = 27,
                };

                btn.Content = textBlock;
                ArticlesButtons.Add(btn);
            }
    }


    private async Task AddToCart(DTOGetArticlesButton article)
    {
        if (_ticket == null) await GenerateTicket();

        var existingItem = cartItems.FirstOrDefault(x => x.ArticleId == article.ArticleId);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            existingItem = new TicketDetails
            {
                TicketId = _ticket.TicketId, Quantity = 1, Price = article.Price, ArticleName = article.Name,
                ArticleId = article.ArticleId
            };

            cartItems.Add(existingItem);
        }

        totalAmount += existingItem.Price;
        cartItemsView?.Refresh();
    }

    public async Task GenerateTicket()
    {
        var dto = new DtoCreateTicket(DateTime.Now, Issuer, totalAmount);
        var result = await _service.SaveTicketAsync(dto);
        if (result != null) _ticket = (Ticket)result.Data;
    }

    public async Task SaveTicketDetails()
    {
        var result = await _service.SaveTicketsDetailsAsync(cartItems);
        _ticket.TotalAmount = totalAmount;
        _service.UpdateTicket(_ticket);
        if (result.Success is true)
        {
            ClearCart();
            _window.Close();
            _windowsManager.CloseCurrentWindowandShowWindow<MainWindow>(_confirmWindow);
        }
    }

    public void ClearCart()
    {
        cartItems.Clear();
        totalAmount = 0;
    }

    private void OpenConfirmWindow()
    {
        var confirmWindow = new ConfirmWindow(this);
        _confirmWindow = confirmWindow;
        confirmWindow.Show();
    }

    private void CloseConfirmWindow()
    {
        if (_confirmWindow == null) return;
        _windowsManager.CloseCurrentWindowandShowWindow<ShoppingCartWindow>(_confirmWindow);
    }

    private void CloseShopingCart()
    {
        _windowsManager.CloseCurrentWindowandShowWindow<MainWindow>(_window);
    }


    public void RemoveFromCart(TicketDetails ticket)
    {
        if (ticket == null) ;
        cartItems.Remove(ticket);
        UpdateTotalPrice();
    }

    private void DecreaseFromCart(TicketDetails ticket)
    {
        if (ticket.Quantity > 1)
            ticket.Quantity -= 1;
        else
            RemoveFromCart(ticket);

        UpdateTotalPrice();
    }

    private void IncreaseQuantity(TicketDetails ticket)
    {
        if (ticket == null) return;
        ticket.Quantity++;
        UpdateTotalPrice();
    }

    private void UpdateTotalPrice()
    {
        decimal total = 0;

        foreach (var item in cartItems) total += item.TotalPrice;

        totalAmount = total;
        cartItemsView.Refresh();
    }

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
}