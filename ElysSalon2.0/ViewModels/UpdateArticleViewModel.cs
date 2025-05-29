using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Application.DTOs.Request.Articles;
using Application.DTOs.Response.Articles;
using Application.Services;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.Utils;
using ElysSalon2._0.views;

namespace ElysSalon2._0.ViewModels;

public class UpdateArticleViewModel : INotifyPropertyChanged
{
    private readonly ArticleAppService _service;
    private readonly UpdateItemWindow _window;
    private DTOGetArticle _article;
    private int _articleId;
    private string _articleName;
    private readonly SemaphoreSlim _updateSemaphore = new SemaphoreSlim(1, 1);

    private int _articleTypeId;

    private ObservableCollection<DTOGetArtType> _articleTypes;

    private string _description;

    private decimal _priceBuy;

    private decimal _priceCost;
    private int _stock;

    public UpdateArticleViewModel(
        int article, UpdateItemWindow window, ArticleAppService service)
    {
        _window = window;
        _articleTypes = new ObservableCollection<DTOGetArtType>();

        onlyDigitsCommand = new RelayCommand<TextCompositionEventArgs>(OnlyDigits);
        ExitCommand = new RelayCommand(Exit);
        updateArticleCommand = new AsyncRelayCommand(UpdateArticle);
        _service = service;
        InitiazlizeAsync(article);
    }

    public ICommand onlyDigitsCommand { get; }

    public int ArticleId
    {
        get => _articleId;
        set
        {
            _articleId = value;
            OnPropertyChanged(nameof(ArticleId));
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

    public ObservableCollection<DTOGetArtType> ArticleTypes
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

    private async void InitiazlizeAsync(int id)
    {
        try
        {
            await LoadItem(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async Task LoadItem(int articleId)
    {
        
        var article = await _service.GetArticleAsync(articleId);
        _article = (DTOGetArticle)article.Data;

        _articleId = articleId;

        Name = _article.Name; 
        ArticleTypeId = _article.Type.ArticleTypeId;
        PriceBuy = _article.PriceBuy;
        Stock = _article.Stock; 
        PriceCost = _article.PriceCost; 
        Description = _article.Description; 

        OnPropertyChanged(nameof(ArticleId));

        var articleTypes = (ObservableCollection<DTOGetArtType>)(await _service.GetTypesAsync()).Data;
        articleTypes.Remove(articleTypes.First(x => x.ArtTypeId == 2));
        articleTypes.Remove(articleTypes.First(x => x.ArtTypeId == 1));

        if (_articleTypes == null)
        {
            _articleTypes = new ObservableCollection<DTOGetArtType>(articleTypes);
        }
        else
        {
            _articleTypes.Clear();
            foreach (var type in articleTypes) _articleTypes.Add(type);
        }
    }

    private async Task UpdateArticle()
    {
        // Si ya hay una operación en curso, salir
        if (!_updateSemaphore.Wait(0))
            return;

        try
        {
            var dto = new DTOUpdateArticle(ArticleId, Name, ArticleTypeId, PriceCost, PriceBuy, Stock, Description);
            var result = await _service.UpdateArticleAsync(dto);

            if (result.Success)
            {
                MessageBox.Show(result.Message, "Operación exitosa", MessageBoxButton.OK);
                Exit();
            }
            else
            {
                MessageBox.Show(result.Message, "Error en operación", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        finally
        {
            _updateSemaphore.Release();
        }
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