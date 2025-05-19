using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.aplication.DTOs.Request.Tickets;
using ElysSalon2._0.aplication.DTOs.Response.Article;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.domain.Entities;
using ElysSalon2._0.views;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.ViewModels;

public class ShoppingCartViewModel : INotifyPropertyChanged
{
    private readonly IArticleService _articleService;
    private readonly IMapper _mapper;
    private readonly Window _window;
    private readonly WindowsManager _windowsManager;
    private ObservableCollection<TicketDetails> _cartItems;
    private readonly ISalesDataService _service;
    private Window _confirmWindow;
    private string _issuer = "messi";
    private Ticket _ticket;
    private decimal _totalAmount;
    public ICollectionView cartItemsView;


    public ShoppingCartViewModel(IArticleService articleService, IMapper mapper, ISalesDataService service,
        WindowsManager windowsManager, Window window)
    {
        _service = service;
        _mapper = mapper;
        _window = window;
        _windowsManager = windowsManager;
        _cartItems = new ObservableCollection<TicketDetails>();
        cartItemsView = CollectionViewSource.GetDefaultView(_cartItems);
        ArticlesButtons = new ObservableCollection<Button>();
        _articleService = articleService;
        _ = LoadButtons();


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

    public decimal TotalAmount
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

    private async Task LoadButtons()
    {
        var articles = await _articleService.GetArticlesToButtons();


        if (articles != null)
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
                    FontSize = 27
                };

                btn.Content = textBlock;
                ArticlesButtons.Add(btn);
            }
    }


    private async Task AddToCart(DTOGetArticlesButton article)
    {
        var existingItem = cartItems.FirstOrDefault(x => x.ArticleId == article.ArticleId);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            existingItem = new TicketDetails
            {
                Quantity = 1,
                Price = article.Price,
                ArticleName = article.Name,
                ArticleId = article.ArticleId,
                Date = DateTime.Now
            };
            cartItems.Add(existingItem);
        }

        TotalAmount += article.Price;
        cartItemsView?.Refresh();
    }

    public async Task SaveTicketDetails()
    {
        var ticketId = await _service.GetLastId<Ticket>();
        var dto = new DtoCreateTicket((string)ticketId.Data, DateTime.Now, Issuer, TotalAmount);
        var result = await _service.Add<Ticket>(_mapper.Map<Ticket>(dto));

        if (result?.Data is Ticket ticket)
        {
            _ticket = ticket;

            // Assign the ticket ID to all cart items
            foreach (var item in cartItems) item.TicketId = _ticket.TicketId;

            // Save all ticket details at once
            var detailsResult = await _service.AddRange(cartItems.ToList());

            if (detailsResult.Success)
            {
                ClearCart();
                _window.Close();
                _windowsManager.CloseCurrentWindowandShowWindow<MainWindow>(_confirmWindow);
            }
        }
    }

    public void ClearCart()
    {
        cartItems.Clear();
        TotalAmount = 0;
    }

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

        TotalAmount = total;
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