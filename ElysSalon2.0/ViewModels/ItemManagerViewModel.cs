﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Application.DTOs.Request.Articles;
using Application.DTOs.Response.Articles;
using Application.Services;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.Utils;
using ElysSalon2._0.views;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.ViewModels;

public class ItemManagerViewModel : INotifyPropertyChanged, IDisposable
{
    #region Properties

    private int _articleTypeId;
    private int _articleTypeSort = 1;
    private string _description;
    private string _priceBuy;
    private string _priceCost;
    private string? _searchText;
    private string _stock;
    private int _articleId;
    private string _articleName;

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            FilterItems();
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
            _articleTypeId = value;
            OnPropertyChanged();
        }
    }

    public int ArticleTypeSort
    {
        get => _articleTypeSort;
        set
        {
            _articleTypeSort = value;
            _ = SortArticles(ArticleTypeSort);
            OnPropertyChanged();
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

    #region Collections

    private ObservableCollection<DTOGetArtType> _articleSortCollection;
    private ObservableCollection<DTOGetArtType> _articleTypesCollection { get; set; }
    private ObservableCollection<DTOGetArticle> _articlesCollection { get; set; }
    private ICollectionView _articlesView;

    public ICollectionView ArticlesView
    {
        get => _articlesView;
        set
        {
            _articlesView = value;
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

    #endregion

    #region Constructor

    private readonly ArticleAppService _service;
    private readonly WindowsManager _windowsManager;
    private Window window { get; }

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
        _ = LoadArticles();

        _service.ReloadItems += OnReloadItems;
        _service.ClearForms += CleanForm;
    }

    #endregion

    #region Commands

    public ICommand addArticleCommand { get; }
    public ICommand deleteArticleCommand { get; }
    public ICommand updateArticleCommand { get; }
    public ICommand onlyDigitsCommand { get; }
    public ICommand ExitCommand { get; }
    public ICommand OpenTypesManagementCommand { get; }

    #endregion

    #region Events

    public void Dispose()
    {
        _service.ReloadItems -= OnReloadItems;
        _service.ClearForms -= CleanForm;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private async void OnReloadItems()
    {
        await System.Windows.Application.Current.Dispatcher.InvokeAsync(async () =>
        {
            await SortArticles(ArticleTypeSort);
        });
    }

    #endregion

    #region CRUD Methods

    private async Task LoadArticles()
    {
        var artsResult = await _service.GetArticlesAsync();
        var artTypesResult = await _service.GetTypesAsync();
        _articleTypesCollection.Clear();
        _articleSortCollection.Clear();

        if (artsResult.Success && artsResult.Data is ObservableCollection<DTOGetArticle> articles)
        {
            _articlesCollection.Clear();
            foreach (var a in articles)
                _articlesCollection.Add(a);
        }

        if (artTypesResult.Success && artTypesResult.Data is ObservableCollection<DTOGetArtType> types)
        {
            foreach (var a in types)
            {
                _articleTypesCollection.Add(a);
                _articleSortCollection.Add(a);
            }

            _articleSortCollection.Remove(_articleSortCollection.FirstOrDefault(x => x.ArtTypeId == 2));
            _articleTypesCollection.Remove(_articleTypesCollection.FirstOrDefault(x => x.ArtTypeId == 1));
        }

        _articleTypeId = 2;
        OnPropertyChanged(nameof(ArticleTypeId));
        OnPropertyChanged(nameof(ArticleTypeSort));

        _articlesView.Refresh();
    }

    private async Task AddArticle()
    {
        var dto = new DTOAddArticle(ArticleName, _articleTypeId, PriceCost ?? "0", PriceBuy ?? "0", Stock ?? "0",
            Description);
        var result = await _service.AddArticleAsync(dto);

        MessageBox.Show(result.Message, result.Success ? "Artículo agregado" : "Error", MessageBoxButton.OK,
            result.Success ? MessageBoxImage.Information : MessageBoxImage.Warning);

        if (result.Success)
            CleanForm();
        await SortArticles(ArticleTypeSort);
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
        _ = SortArticles(typeSort);
    }

    #endregion

    #region UI methods

    private void onlyDigits(TextCompositionEventArgs e)
    {
        UIElementsUtil.NumericOnly_PreviewTextInput(e.OriginalSource as UIElement, e);
    }

    private bool FilterArticles(object item) //Filter the articles by name on the search bar
    {
        if (string.IsNullOrEmpty(SearchText))
            return true;

        var article = item as DTOGetArticle;
        return article.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
    }

    private async Task SortArticles(int typeId)
    {
        await LoadArticles();
        if (typeId != 1)
        {
            var sorted = _articlesCollection.Where(item => item.Type.ArticleTypeId.Equals(typeId)).ToList();
            _articlesCollection.Clear();

            foreach (var item in sorted) _articlesCollection.Add(item);

            OnPropertyChanged(nameof(ArticleCollection));
        }

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

    private void openTypesManagement()
    {
        _windowsManager.NavigateToWindow<TypeArticleWindow>();
    }

    private void FilterItems()
    {
        _articlesView.Refresh();
    }

    #endregion
}