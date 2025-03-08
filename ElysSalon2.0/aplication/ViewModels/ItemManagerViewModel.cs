﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Services;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.ViewModels;

public class ItemManagerViewModel : INotifyPropertyChanged
{
    private readonly IArticleRepository _articleRepo;
    private readonly IArticleTypeRepository _artTypeRepo;
    private readonly IArticleService _service;
    private readonly WindowsManager _windowsManager;
    private Window window { get; set; }
    private int _articleId;

    private string _articleName;

    private ObservableCollection<ArticleType> _articleSortCollection;

    private ICollectionView _articlesView;

    private int _articleTypeId = 2;

    private int _articleTypeSort = 1;

    private string _description;

    private string _priceBuy;

    private string _priceCost;
    private string? _searchText;


    private int _stock;

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

    private ObservableCollection<ArticleType> _articleTypesCollection { get; set; }
    private ObservableCollection<Article> _articlesCollection { get; set; }

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

    public string articleName
    {
        get => _articleName;
        set
        {
            SetField(ref _articleName, value);
            OnPropertyChanged();
        }
    }

    public int articleTypeId
    {
        get => _articleTypeId;
        set
        {
            SetField(ref _articleTypeId, value);
            OnPropertyChanged();
        }
    }

    public int articleTypeSort
    {
        get => _articleTypeSort;
        set
        {
            _articleTypeSort = value;
            OnPropertyChanged();
            SortArticles(_articleTypeSort);
        }
    }

    public string priceCost
    {
        get => _priceCost;
        set
        {
            _priceCost = value;
            OnPropertyChanged();
        }
    }

    public string priceBuy
    {
        get => _priceBuy;
        set
        {
            SetField(ref _priceBuy, value);
            OnPropertyChanged();
        }
    }

    public int stock
    {
        get => _stock;
        set
        {
            SetField(ref _stock, value);
            OnPropertyChanged();
        }
    }

    public string description
    {
        get => _description;
        set
        {
            SetField(ref _description, value);
            OnPropertyChanged();
        }
    }


    public ObservableCollection<Article> articleCollection
    {
        get => _articlesCollection;
        set
        {
            _articlesCollection = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ArticleType> articleTypesCollection
    {
        get => _articleTypesCollection;
        set
        {
            _articleTypesCollection = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ArticleType> articleSortCollection
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
    public ICommand exitCommand { get; }
    public ICommand OpenTypesManagementCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void onlyDigits(TextCompositionEventArgs e)
    {
        UIElementsUtil.NumericOnly_PreviewTextInput(e.OriginalSource as UIElement, e);
    }

    public ItemManagerViewModel(IArticleRepository articleRepository, IArticleTypeRepository articleTypeRepository,
        Window windows, IArticleService service, WindowsManager windowsManager)
    {
        _windowsManager = windowsManager;
        _service = service;
        _artTypeRepo = articleTypeRepository;
        _articleRepo = articleRepository;
        window = windows;
        _articlesCollection = new ObservableCollection<Article>();
        _articleTypesCollection = new ObservableCollection<ArticleType>();
        _articleSortCollection = new ObservableCollection<ArticleType>();

        _articlesView = CollectionViewSource.GetDefaultView(_articlesCollection);
        _articlesView.Filter = FilterArticles;

        addArticleCommand = new AsyncRelayCommand(AddArticle);
        deleteArticleCommand = new AsyncRelayCommand<Article>(DeleteArticle);
        updateArticleCommand = new AsyncRelayCommand<Article>(EditArticle);
        OpenTypesManagementCommand = new AsyncRelayCommand(openTypesManagement);

        onlyDigitsCommand = new RelayCommand<TextCompositionEventArgs>(onlyDigits);
        exitCommand = new RelayCommand(exit);
        LoadArticles();

        _service.reloadItems += async () => await SortArticles(articleTypeSort);
        _service.clearForms += cleanForm;
    }


    public async Task LoadArticles() //Load the articles and article's types to the view
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

        articleTypeId = 2;

        _articlesView?.Refresh();
    }

    private async Task AddArticle()
    {
        var dto = new DTOAddArticle(articleName, articleTypeId, priceBuy,
            priceCost, stock, description);
        await _service.AddArticle(dto);
      
    }


    private Task EditArticle(Article article)
    {
        var updateWindow = new UpdateItemWindow();
        var updateViewModel = new UpdateArticleViewModel(_artTypeRepo, _articleRepo, article, updateWindow, _service);
        updateWindow.DataContext = updateViewModel;
        updateWindow.Show();
        return Task.CompletedTask;
    }

    private async Task DeleteArticle(Article article)
    {
        int typeSort = articleTypeSort;
        await _service.DeleteArticle(article.ArticleId);
        MessageBox.Show("Artículo eliminado con éxito");
       
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
            var sorted = _articlesCollection.Where(item => item.ArticleTypeId.Equals(typeId)).ToList();
            _articlesCollection.Clear();
            foreach (var item in sorted) _articlesCollection.Add(item);

            _articlesView.Refresh();
        }
    }

    private async Task openTypesManagement()
    {
        _windowsManager.NavigateToWindow<TypeArticleWindow>();
    }

    private void FilterItems()
    {
        _articlesView.Refresh();
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


    public void exit() //exit to Item Manager
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