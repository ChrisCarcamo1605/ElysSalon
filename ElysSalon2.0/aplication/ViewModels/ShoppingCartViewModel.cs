using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.ViewModels;

public class ShoppingCartViewModel : INotifyPropertyChanged
{
    private readonly IArticleRepository _articleRepository;

    private readonly IMapper _mapper;
    private ObservableCollection<TicketDetails> _cartItems;
    private decimal _totalAmount;
    public ICollectionView cartItemsView;

    public ShoppingCartViewModel(IArticleRepository articleRepository, IMapper mapper)
    {
        _mapper = mapper;
        _cartItems = new ObservableCollection<TicketDetails>();
        cartItemsView = CollectionViewSource.GetDefaultView(_cartItems);
        ArticlesButtons = new ObservableCollection<Button>();
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        loadButtons();

        RemoveFromCartCommand = new RelayCommand<TicketDetails>(RemoveFromCart);
        DecreaseQuantityCommand = new RelayCommand<TicketDetails>(DecreaseFromCart);
        IncreaseQuantityCommand = new RelayCommand<TicketDetails>(IncreaseQuantity);
    }

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

    public ObservableCollection<Button> ArticlesButtons { get; set; }


    public event PropertyChangedEventHandler? PropertyChanged;

    private async Task loadButtons()
    {
        var articles = await _articleRepository.GetArticlesToButton();

        if (articles != null && articles.Any())
            foreach (var article in articles)
            {
                var btn = new Button
                {
                    Tag = article.articleId,
                    Style = (Style)Application.Current.FindResource("articlesBtn"),
                    Padding = new Thickness(10)
                };
                btn.Click += async (e, s) => { addToCart(article); };
                var textBlock = new TextBlock
                {
                    Text = article.Name,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                btn.Content = textBlock;
                ArticlesButtons.Add(btn);
            }
    }


    private void addToCart(DTOGetArticlesButton article)
    {
        var existingItem = cartItems.FirstOrDefault(x => x.ArticleId == article.articleId);

        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            existingItem = new TicketDetails
            {
                TicketId = "1", ArticleId = article.articleId, Quantity = 1, Price = article.price,
                Article = _mapper.Map<Article>(article)
            };

            cartItems.Add(existingItem);
        }

        totalAmount += existingItem.Price;
        cartItemsView?.Refresh();
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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


    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}