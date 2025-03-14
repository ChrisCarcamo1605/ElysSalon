using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.Core.aplication.DTOs.DTOArticle;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.aplication.Ports.Services;
using ElysSalon2._0.Core.aplication.Utils;
using ElysSalon2._0.Core.domain.Entities;

namespace ElysSalon2._0.adapters.InBound.UI.ViewModels;

public class UpdateArticleViewModel : INotifyPropertyChanged
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleTypeRepository _articleTypeRepository;
    private readonly IArticleService _service;
    private readonly UpdateItemWindow _window;
    private Article _article;
    private int _articleId;
    private string _articleName;

    private int _articleTypeId;

    private ObservableCollection<ArticleType> _articleTypes;

    private string _description;

    private decimal _priceBuy;

    private decimal _priceCost;
    private int _stock;

    public UpdateArticleViewModel(IArticleTypeRepository articleTypeRepository, IArticleRepository articleRepository,
        int article, UpdateItemWindow window, IArticleService service)
    {
        _window = window;
        _articleTypes = new ObservableCollection<ArticleType>();
        _articleRepository = articleRepository;
        _articleTypeRepository = articleTypeRepository;
        onlyDigitsCommand = new RelayCommand<TextCompositionEventArgs>(OnlyDigits);
        ExitCommand = new RelayCommand(Exit);
        updateArticleCommand = new AsyncRelayCommand(UpdateArticle);
        _service = service;
        LoadItem(article);
    }

    public ICommand onlyDigitsCommand { get; }

    public int articleId
    {
        get => _articleId;
        set
        {
            _articleId = value;
            OnPropertyChanged();
        }
    }

    public int articleTypeId
    {
        get => _articleTypeId;
        set
        {
            _articleTypeId = value;
            OnPropertyChanged();
        }
    }

    public string Name
    {
        get => _articleName;
        set => SetField(ref _articleName, value);
    }

    public decimal PriceBuy
    {
        get => _priceBuy;
        set
        {
            _priceBuy = value;
            OnPropertyChanged();
        }
    }

    public int Stock
    {
        get => _stock;
        set
        {
            _stock = value;
            OnPropertyChanged();
        }
    }

    public decimal PriceCost
    {
        get => _priceCost;
        set
        {
            _priceCost = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ArticleType> ArticleTypes
    {
        get => _articleTypes;
        set
        {
            _articleTypes = value;
            OnPropertyChanged();
        }
    }

    public ICommand updateArticleCommand { get; }
    public ICommand ExitCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public event Action? reloadItems;

    private async Task LoadItem(int articleId)
    {
        _article = await _articleRepository.GetArticleAsync(articleId);

        _articleId = articleId;
        _articleName = _article.Name;
        _articleTypeId = _article.ArticleTypeId;
        _priceBuy = _article.PriceBuy;
        _stock = _article.Stock;
        _priceCost = _article.PriceCost;
        _description = _article.Description;

        var articleTypes = await _articleTypeRepository.GetTypesAsync();
        articleTypes.Remove(articleTypes.First(x => x.ArticleTypeId == 2));
        articleTypes.Remove(articleTypes.First(x => x.ArticleTypeId == 1));

        if (_articleTypes == null)
        {
            _articleTypes = new ObservableCollection<ArticleType>(articleTypes);
        }
        else
        {
            _articleTypes.Clear();
            foreach (var type in articleTypes) _articleTypes.Add(type);
        }
    }

    private async Task UpdateArticle()
    {
        var dto = new DTOUpdateArticle(articleId, Name, articleTypeId, PriceCost, PriceBuy, Stock, Description);
        var result = await _service.UpdateArticle(dto);

        if (result.Success) Exit();

        MessageBox.Show(result.Message);
    }

    private void Exit()
    {
        _window.Close();
    }

    private void OnlyDigits(TextCompositionEventArgs e)
    {
        UIElementsUtil.NumericOnly_PreviewTextInput(e.OriginalSource as UIElement, e);
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