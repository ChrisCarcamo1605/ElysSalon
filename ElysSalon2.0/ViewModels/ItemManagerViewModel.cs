using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Application.DTOs.Request.Articles;
using Application.DTOs.Response.Articles;
using Application.Services;
using CommunityToolkit.Mvvm.Input;
using Core.Domain.Entities;
using ElysSalon2._0.Utils;
using ElysSalon2._0.views;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.ViewModels;

public class ItemManagerViewModel : INotifyPropertyChanged
{
    private readonly WindowsManager _windowsManager;
    private readonly ArticleAppService _service;
    private int _articleId;

    private string _articleName;

    private ObservableCollection<DTOGetArtType> _articleSortCollection;

    private ICollectionView _articlesView;

    private int _articleTypeId = 2;

    private int _articleTypeSort = 1;

    private string _description;

    private string _priceBuy;

    private string _priceCost;
    private string? _searchText;
    private string _stock;


    private Window window { get; }

    public string searchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            FilterItems();
        }
    }

    private ObservableCollection<DTOGetArtType> _articleTypesCollection { get; set; }
    private ObservableCollection<DTOGetArticle> _articlesCollection { get; set; }

    public ICollectionView ArticlesView
    {
        get => _articlesView;
        set
        {
            _articlesView = value;
            OnPropertyChanged();
        }
    }


    public int articleId
    {
        get => _articleId;
        set
        {
            SetField(ref _articleId, value);
            OnPropertyChanged();
        }
    }

    public string ArticleName
    {
        get => _articleName;
        set
        {
            SetField(ref _articleName, value);
            OnPropertyChanged();
        }
    }

    public int ArticleTypeId
    {
        get => _articleTypeId;
        set
        {
            SetField(ref _articleTypeId, value);
            OnPropertyChanged();
        }
    }

    public int ArticleTypeSort
    {
        get => _articleTypeSort;
        set
        {
            _articleTypeSort = value;
            OnPropertyChanged();
            SortArticles(_articleTypeSort);
        }
    }

    public string PriceCost
    {
        get => _priceCost;
        set
        {
            _priceCost = value;
            OnPropertyChanged();
        }
    }

    public string PriceBuy
    {
        get => _priceBuy;
        set
        {
            SetField(ref _priceBuy, value);
            OnPropertyChanged();
        }
    }

    public string Stock
    {
        get => _stock;
        set
        {
            SetField(ref _stock, value);
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            SetField(ref _description, value);
            OnPropertyChanged();
        }
    }


    public ObservableCollection<DTOGetArticle> ArticleCollection
    {
        get => _articlesCollection;
        set
        {
            _articlesCollection = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DTOGetArtType> ArticleTypesCollection
    {
        get => _articleTypesCollection;
        set
        {
            _articleTypesCollection = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<DTOGetArtType> ArticleSortCollection
    {
        get => _articleSortCollection;

        set
        {
            SetField(ref _articleSortCollection, value);
            OnPropertyChanged();
        }
    }

    public ICommand addArticleCommand { get; }
    public ICommand deleteArticleCommand { get; }
    public ICommand updateArticleCommand { get; }
    public ICommand onlyDigitsCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand OpenTypesManagementCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public ItemManagerViewModel(
        Window windows, ArticleAppService articleService, WindowsManager windowsManager)
    {
        _windowsManager = windowsManager;
        _service = articleService;
        window = windows;
        _articlesCollection = new ObservableCollection<DTOGetArticle>();
        _articleTypesCollection = new ObservableCollection<DTOGetArtType>();
        _articleSortCollection = new ObservableCollection<DTOGetArtType>();

        _articlesView = CollectionViewSource.GetDefaultView(_articlesCollection);
        _articlesView.Filter = FilterArticles;

        addArticleCommand = new AsyncRelayCommand(AddArticle);
        deleteArticleCommand = new AsyncRelayCommand<DTOGetArticle>(DeleteArticle);
        updateArticleCommand = new RelayCommand<DTOGetArticle>(EditArticle);
        OpenTypesManagementCommand = new RelayCommand(openTypesManagement);

        onlyDigitsCommand = new RelayCommand<TextCompositionEventArgs>(onlyDigits);
        ExitCommand = new RelayCommand(Exit);
        LoadArticles();

        _service.reloadItems += async () => await SortArticles(ArticleTypeSort);
        _service.clearForms += CleanForm;
    }

    private void onlyDigits(TextCompositionEventArgs e)
    {
        UIElementsUtil.NumericOnly_PreviewTextInput(e.OriginalSource as UIElement, e);
    }


    public async Task LoadArticles() //Load the articles and article's types to the view
    {
        _articleSortCollection = (ObservableCollection<DTOGetArtType>)(await _service.GetTypesAsync()).Data;
        OnPropertyChanged(nameof(ArticleSortCollection));

        var articleTypes = (ObservableCollection<DTOGetArtType>)(await _service.GetTypesAsync()).Data;
        var articles = (ObservableCollection<DTOGetArticle>)(await _service.GetArticlesAsync()).Data;


        _articleSortCollection.Remove(articleTypes.First(x => x.ArtTypeId == 2));
        articleTypes.Remove(articleTypes.First(x => x.ArtTypeId == 1));


        if (_articleTypesCollection == null)
        {
            _articleTypesCollection = articleTypes;
        }
        else
        {
            _articleTypesCollection.Clear();

            foreach (var articleType in articleTypes) _articleTypesCollection.Add(articleType);
        }


        if (_articlesCollection == null)
        {
            _articlesCollection = articles;
            _articlesView = CollectionViewSource.GetDefaultView(_articlesCollection);
        }
        else
        {
            _articlesCollection.Clear();
            foreach (var article in articles) _articlesCollection.Add(article);
        }

        ArticleTypeId = 2;

        _articlesView?.Refresh();
    }

    private async Task AddArticle()
    {
        var dto = new DTOAddArticle(ArticleName,
            ArticleTypeId,
            PriceCost ?? "0",
            PriceBuy ?? "0",
            Stock ?? "0", Description);

        var result = await _service.AddArticleAsync(dto);
        if (result.Success)
        {
            MessageBox.Show(result.Message, "Articulo agregado", MessageBoxButton.OK, MessageBoxImage.Information);
            CleanForm();
            return;
        }
        else
        {
            MessageBox.Show(result.Message, "Error de formulario", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }

    private void EditArticle(DTOGetArticle article)
    {
        var updateWindow = new UpdateItemWindow(article.ArticleId, _service);
        updateWindow.Show();
    }

    private async Task DeleteArticle(DTOGetArticle article)
    {
        var typeSort = ArticleTypeSort;
        var result = await _service.DeleteArticleAsync(article.ArticleId);
        MessageBox.Show(result.Message);
        SortArticles(typeSort);
    }

    private bool FilterArticles(object item) //Filter the articles by name on the search bar
    {
        if (string.IsNullOrEmpty(searchText))
            return true;

        var article = item as Article;
        return article.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase);
    }

    private async Task SortArticles(int typeId) //Sort the articles by type
    {
        await LoadArticles();

        if (typeId != 1)
        {
            var sorted = _articlesCollection.Where(item => item.Type.ArticleTypeId.Equals(typeId)).ToList();
            _articlesCollection.Clear();
            foreach (var item in sorted) _articlesCollection.Add(item);

            _articlesView.Refresh();
        }
    }

    private void openTypesManagement()
    {
        _windowsManager.NavigateToWindow<TypeArticleWindow>();
    }

    private void FilterItems()
    {
        _articlesView.Refresh();
    }

    public void CleanForm()
    {
        ArticleTypeId = 2;
        ArticleName = "";
        PriceBuy = "";
        PriceCost = "";
        Stock = "";
        Description = "";
    }

    public void Exit()
    {
        _windowsManager.CloseCurrentWindowandShowWindow<AdminWindow>(window);
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