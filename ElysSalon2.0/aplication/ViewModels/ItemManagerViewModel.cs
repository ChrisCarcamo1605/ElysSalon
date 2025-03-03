using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.adapters.OutBound;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;
using Microsoft.IdentityModel.Tokens;
using WinRT;

namespace ElysSalon2._0.aplication.ViewModels;

public class ItemManagerViewModel : INotifyPropertyChanged
{
    private string? _searchText;

    public string searchText
    {
        get { return _searchText; }
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
    private WindowsManager _windowsManager;
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

    private int _articleTypeId = 2;

    public int articleTypeId
    {
        get { return _articleTypeId; }
        set
        {
            SetField(ref _articleTypeId, value);
            OnPropertyChanged(nameof(articleTypeId));
        }
    }

    private int _articleTypeSort = 1;

    public int articleTypeSort
    {
        get { return _articleTypeSort; }
        set
        {
            _articleTypeSort = value;
            OnPropertyChanged(nameof(articleTypeSort));
            SortArticles(_articleTypeSort);
        }
    }

    private string _priceCost;

    public string priceCost
    {
        get { return _priceCost; }
        set
        {
            _priceCost = value;
            OnPropertyChanged(nameof(priceCost));
        }
    }

    private string _priceBuy;

    public string priceBuy
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
            OnPropertyChanged(nameof(articleTypesCollection));
        }
    }

    private ObservableCollection<ArticleType> _articleSortCollection;

    public ObservableCollection<ArticleType> articleSortCollection
    {
        get { return _articleSortCollection; }

        set
        {
            SetField(ref _articleSortCollection, value);
            OnPropertyChanged(nameof(articleSortCollection));
        }
    }


    public ICommand addArticleCommand { get; }
    public ICommand deleteArticleCommand { get; }
    public ICommand updateArticleCommand { get; }

    public ItemManagerViewModel(IArticleRepository articleRepository, IArticleTypeRepository articleTypeRepository,
        WindowsManager windows)
    {
        _artTypeRepo = articleTypeRepository;
        _articleRepo = articleRepository;
        _windowsManager = windows;
        _articlesCollection = new ObservableCollection<Article>();
        _articleTypesCollection = new ObservableCollection<ArticleType>();
        _articleSortCollection = new ObservableCollection<ArticleType>();
        LoadArticles();

        _articlesView = CollectionViewSource.GetDefaultView(_articlesCollection);
        _articlesView.Filter = FilterArticles;

        addArticleCommand = new AsyncRelayCommand(AddArticle);
        deleteArticleCommand = new AsyncRelayCommand<Article>(DeleteArticle);
        updateArticleCommand = new AsyncRelayCommand<Article>(EditArticle);
    }


    public async Task LoadArticles()
    {
        _articleSortCollection = await _artTypeRepo.getTypes();
        OnPropertyChanged(nameof(articleSortCollection));

        var articleTypes = await _artTypeRepo.getTypes();
        var articles = await _articleRepo.GetArticles();


        _articleSortCollection.Remove(articleTypes.First(x => x.ArticleTypeId == 2));
        articleTypes.Remove(articleTypes.First(x => x.ArticleTypeId == 1));


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

        articleTypeId = 2;
        _articlesView?.Refresh();
    }

    private bool FilterArticles(object item)
    {
        if (string.IsNullOrEmpty(searchText))
            return true;

        var article = item as Article;
        return article.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase);
    }

    private async void SortArticles(int typeId)
    {
        await LoadArticles();

        if (typeId != 1)
        {
            var sorted = _articlesCollection.Where(item => item.ArticleTypeId.Equals(typeId)).ToList();
            _articlesCollection.Clear();
            foreach (var item in sorted)
            {
                _articlesCollection.Add(item);
            }

            _articlesView.Refresh();
        }
    }

    private void FilterItems()
    {
        _articlesView.Refresh();
    }

    private async Task AddArticle()
    {
        if (string.IsNullOrEmpty(articleName))
        {
            MessageBox.Show("Ingrese un nombre");
            return;
        }

        if (articleTypeId == 2)
        {
            MessageBox.Show("Seleccione un tipo de artículo");
            return;
        }


        if (!int.TryParse(stock.ToString(), out _))
        {
            MessageBox.Show("El stock debe ser un número entero válido.");
            return;
        }

        if (stock <= 0)
        {
            MessageBox.Show("Ingrese la cantidad en stock.");
            return;
        }

        var newArticle = new Article
        {
            Name = articleName,
            ArticleTypeId = articleTypeId,
            PriceBuy = Math.Round(Convert.ToDecimal(priceBuy), 2),
            PriceCost = Math.Round(Convert.ToDecimal(priceCost), 2),
            Stock = stock,
            Description = description
        };

        await _articleRepo.AddArticle(newArticle);

        MessageBox.Show("Artículo guardado con éxito!");
        await LoadArticles();
        cleanForm();
    }


    public void cleanForm()
    {
        articleTypeId = 2;
        articleName = "";
        priceBuy = "";
        priceCost = "";
        stock = 0;
        description = "";
    }


    private async Task EditArticle(Article article)
    {
        var updateWindow = new UpdateItemWindow();
        var updateModelView = new UpdateArticleViewModel(_artTypeRepo, _articleRepo, article, updateWindow);
        updateWindow.DataContext = updateModelView;
        updateModelView.reloadItems += async () => { await LoadArticles(); };
        updateWindow.Show();
    }


    private async Task DeleteArticle(Article article)
    {
        if (article == null) throw new NullReferenceException("Articulo nulo");
        await _articleRepo.DeleteArticle(article.ArticleId);
        await LoadArticles();
        cleanForm();
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