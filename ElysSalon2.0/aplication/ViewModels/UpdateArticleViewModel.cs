using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Services;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.aplication.ViewModels;

public class UpdateArticleViewModel : INotifyPropertyChanged
{
    private readonly Article _article;
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleTypeRepository _articleTypeRepository;
    private readonly UpdateItemWindow _window;
    private int _articleId;
    private IArticleService _service;
    private string _articleName;

    private int _articleTypeId;

    private ObservableCollection<ArticleType> _articleTypes;

    private string _description;

    private decimal _priceBuy;

    private decimal _priceCost;
    public ICommand onlyDigitsCommand { get; }
    private int _stock;

    public UpdateArticleViewModel(IArticleTypeRepository articleTypeRepository, IArticleRepository articleRepository,
        Article article, UpdateItemWindow window, IArticleService service)
    {
        _window = window;
        _article = article;
        _articleTypes = new ObservableCollection<ArticleType>();
        _articleRepository = articleRepository;
        _articleTypeRepository = articleTypeRepository;
        onlyDigitsCommand = new RelayCommand<TextCompositionEventArgs>(onlyDigits);
        exitCommand = new AsyncRelayCommand(Exit);
        updateArticleCommand = new AsyncRelayCommand(UpdateArticle);


        _service = service;
        LoadItem(_article);
    }

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

    public string articleName
    {
        get => _articleName;
        set
        {
            _articleName = value;
            OnPropertyChanged();
        }
    }

    public decimal priceBuy
    {
        get => _priceBuy;
        set
        {
            _priceBuy = value;
            OnPropertyChanged();
        }
    }

    public int stock
    {
        get => _stock;
        set
        {
            _stock = value;
            OnPropertyChanged();
        }
    }

    public decimal priceCost
    {
        get => _priceCost;
        set
        {
            _priceCost = value;
            OnPropertyChanged();
        }
    }

    public string description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ArticleType> articleTypes
    {
        get => _articleTypes;
        set
        {
            _articleTypes = value;
            OnPropertyChanged();
        }
    }

    public ICommand updateArticleCommand { get; }
    public ICommand exitCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public event Action? reloadItems;

    private async Task LoadItem(Article article)
    {
        _articleId = article.ArticleId;
        _articleName = article.Name;
        _articleTypeId = article.ArticleTypeId;
        _priceBuy = article.PriceBuy;
        _stock = article.Stock;
        _priceCost = Convert.ToDecimal(article.PriceCost);
        _description = article.Description;

        var articleTypes = await _articleTypeRepository.getTypes();
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
        _article.Name = _articleName;
        _article.ArticleTypeId = _articleTypeId;
        _article.PriceBuy = _priceBuy;
        _article.Stock = _stock;
        _article.PriceCost = _priceCost;
        _article.Description = _description;
        _article.ArticleTypeId = _articleTypeId;
        _article.PriceBuy = _priceBuy;
        
        _service.UpdateArticle(_article);
        Exit();
    }

    private async Task Exit()
    {
        _window.Close();
    }


    private void onlyDigits(TextCompositionEventArgs e)
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