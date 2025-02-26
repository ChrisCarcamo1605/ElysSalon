using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.OutBound;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace ElysSalon2._0.aplication.ViewModels;

public class ItemManagerViewModel : INotifyPropertyChanged
{
    private string? _searchText;

    public string searchText
    {
        get
        {
            
            return _searchText;
        }
        set
        {
            _searchText = value;
            OnPropertyChanged(nameof(searchText));
            FilterItems();
        }
    }

    private IArticleRepository _articleRepo;
    private IArticleTypeRepository _artTypeRepo;
    private ObservableCollection<ArticleType> _articleTypesCollection { get; set; }
    private ObservableCollection<Article> _articlesCollection { get; set; }

    private int _articleId;

    private ICollectionView _articlesView;
    public ICollectionView ArticlesView
    {
        get { return _articlesView; }
        set
        {
            _articlesView = value;
            OnPropertyChanged(nameof(ArticlesView));
        }
    }


    public int articleId
    {
        get { return _articleId; }
        set
        {
            SetField(ref _articleId, value);
            OnPropertyChanged(nameof(articleId));
        }
    }

    private string _articleName;

    public string articleName
    {
        get { return _articleName; }
        set
        {
            SetField(ref _articleName, value);
            OnPropertyChanged(nameof(articleName));
        }
    }

    private int _articleTypeId;

    public int articleTypeId
    {
        get { return _articleTypeId; }
        set
        {
            SetField(ref _articleTypeId, value);
            OnPropertyChanged(nameof(articleTypeId));
        }
    }

    private decimal _priceCost;

    public decimal priceCost
    {
        get { return _priceCost; }
        set
        {
            SetField(ref _priceCost, value);
            OnPropertyChanged(nameof(priceCost));
        }
    }

    private decimal _priceBuy;

    public decimal priceBuy
    {
        get { return _priceBuy; }
        set
        {
            SetField(ref _priceBuy, value);
            OnPropertyChanged(nameof(priceBuy));
        }
    }

    private int _stock;

    public int stock
    {
        get { return _stock; }
        set
        {
            SetField(ref _stock, value);
            OnPropertyChanged(nameof(stock));
        }
    }

    private string _description;

    public string description
    {
        get { return _description; }
        set
        {
            SetField(ref _description, value);
            OnPropertyChanged(nameof(description));
        }
    }


    public ObservableCollection<Article> articleCollection
    {
        get { return _articlesCollection; }
        set
        {
            _articlesCollection = value;
            OnPropertyChanged(nameof(articleCollection));
        }
    }

    public ObservableCollection<ArticleType> articleTypesCollection
    {
        get { return _articleTypesCollection; }
        set
        {
            _articleTypesCollection = value;
            OnPropertyChanged(nameof(_articlesView));
        }
    }


    public ICommand addArticleCommand { get; }
    public ICommand deleteArticleCommand { get; }
    public ICommand updateArticleCommand { get; }

    public ItemManagerViewModel(IArticleRepository articleRepository, IArticleTypeRepository articleTypeRepository)
    {
        _artTypeRepo = articleTypeRepository;
        _articleRepo = articleRepository;

        _articlesCollection = new ObservableCollection<Article>();
        _articleTypesCollection = new ObservableCollection<ArticleType>();
        _articlesView = CollectionViewSource.GetDefaultView(_articlesCollection);
        _articlesView.Filter = FilterArticles;


        addArticleCommand = new AsyncRelayCommand(addArticle);
        deleteArticleCommand = new AsyncRelayCommand<Article>(DeleteArticle);
        updateArticleCommand = new AsyncRelayCommand<Article>(UpdateArticle);
    }


    public async Task loadArticles()
    {
        var articles = await _articleRepo.GetArticles();
        var articleTypes = await _artTypeRepo.getTypes();


        if (_articleTypesCollection == null)
        {
            _articleTypesCollection = articleTypes;
        }
        else
        {
            _articleTypesCollection.Clear();

            foreach (var articleType in articleTypes)
            {
                _articleTypesCollection.Add(articleType);
            }
        }


        if (_articlesCollection == null)
        {
            _articlesCollection = articles;
            _articlesView = CollectionViewSource.GetDefaultView(_articlesCollection);
        }
        else
        {
            _articlesCollection.Clear();
            foreach (var article in articles)
            {
                _articlesCollection.Add(article);
            }
        }

        _articlesView?.Refresh();
    }

    private bool FilterArticles(object item)
    {
        if (string.IsNullOrEmpty(searchText))
            return true;

        var article = item as Article;
        return article.ArticleName.Contains(searchText, StringComparison.OrdinalIgnoreCase);
    }
    private void FilterItems()
    {
        _articlesView.Refresh(); 
    }

    private async Task addArticle()
    {
        if (articleName.IsNullOrEmpty())
        {
            MessageBox.Show("Ingrese un nombre");
            return;
        }

        if (articleTypeId <= 0)
        {
            MessageBox.Show("Seleccione un tipo de articulo");
            return;
        }

        if (priceBuy <= 0)
        {
            MessageBox.Show("Ingrese un precio de venta");
            return;
        }

        if (stock <= 0)
        {
            MessageBox.Show("Ingrese la cantidad en stock");
            return;
        }

        var newArticle = new Article
        {
            ArticleName = articleName,
            ArticleTypeId = articleTypeId,
            PriceBuy = priceBuy,
            PriceCost = priceCost,
            Stock = stock,
            Description = description
        };
        await _articleRepo.AddArticle(newArticle);
        MessageBox.Show("Articulo guardado con exito!");
        await loadArticles();
    }

    private async Task UpdateArticle(Article article)
    {
        if (article == null) throw new NullReferenceException("Articulo nulo");
        _articleRepo.UpdateArticle(article);
        await loadArticles();
    }

    private async Task DeleteArticle(Article article)

    {
        if (article == null) throw new NullReferenceException("Articulo nulo");
        await _articleRepo.DeleteArticle(article.ArticleId);
        await loadArticles();
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