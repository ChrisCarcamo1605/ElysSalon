using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.DTOs.DTOTicketDetails;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.Management;

public class ButtonManager : INotifyPropertyChanged
{
    private IArticleRepository _articleRepository;
    public ICollectionView cartItemsView;
    private DataGrid _grid;
    private ObservableCollection<DtoCreateTicketDetails> _cartItems;

    public ObservableCollection<DtoCreateTicketDetails> cartItems
    {
        get => _cartItems;
        set => OnPropertyChanged(nameof(cartItems));
    }

    public ObservableCollection<Button> ArticlesButtons { get; set; }

    public ButtonManager(IArticleRepository articleRepository, DataGrid grid)
    {
        _grid = grid;
        _cartItems = new ObservableCollection<DtoCreateTicketDetails>();
        ArticlesButtons = new ObservableCollection<Button>();
        _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        loadButtons();
    }

    public void loadButtons()
    {
        var articles = _articleRepository.GetArticlesToButton();

        if (articles != null && articles.Any())
            foreach (var article in articles)
            {
                var btn = new Button
                {
                    Tag = article.articleId,
                    Style = (Style)Application.Current.FindResource("articlesBtn"),
                    Padding = new Thickness(10),
                };
                btn.Click += async (e, s) => { addToCart(article); };
                var textBlock = new TextBlock
                {
                    Text = article.articleName,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };

                btn.Content = textBlock;
                ArticlesButtons.Add(btn);
            }
    }


    public void addToCart(DTOGetArticlesButton article)
    {
        var existingItem = _cartItems.FirstOrDefault(x => x.Article.articleId == article.articleId);

        if (existingItem != null)
        {
            existingItem.quantity++;
        }
        else
        {
            existingItem = new DtoCreateTicketDetails(article.articleId, null,
                _articleRepository.GetArticle(article.articleId), 1, article.price);

            _cartItems.Add(existingItem);
        }

        cartItemsView = CollectionViewSource.GetDefaultView(_cartItems);
        _grid.ItemsSource = cartItemsView;
        cartItemsView?.Refresh();
    }


    public void loadCartItems()
    {
    }

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
}