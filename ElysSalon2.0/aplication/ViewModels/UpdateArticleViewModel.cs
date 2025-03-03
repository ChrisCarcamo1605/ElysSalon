using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.adapters.OutBound;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.aplication.ViewModels;

public class UpdateArticleViewModel : INotifyPropertyChanged
{
    private IArticleTypeRepository _articleTypeRepository;
    private IArticleRepository _articleRepository;
    private Article _article;
    private int _articleId;

    public int articleId
    {
        get { return _articleId; }
        set
        {
            _articleId = value;
            OnPropertyChanged(nameof(articleId));
        }
    }

    private int _articleTypeId;

    public int articleTypeId
    {
        get { return _articleTypeId; }
        set
        {
            _articleTypeId = value;
            OnPropertyChanged(nameof(articleTypeId));
        }
    }

    private string _articleName;

    public string articleName
    {
        get { return _articleName; }
        set
        {
            _articleName = value;
            OnPropertyChanged(nameof(articleName));
        }
    }

    private decimal _priceBuy;

    public decimal priceBuy
    {
        get { return _priceBuy; }
        set
        {
            _priceBuy = value;
            OnPropertyChanged(nameof(priceBuy));
        }
    }

    private int _stock;

    public int stock
    {
        get { return _stock; }
        set
        {
            _stock = value;
            OnPropertyChanged(nameof(stock));
        }
    }

    private decimal _priceCost;

    public decimal priceCost
    {
        get { return _priceCost; }
        set
        {
            _priceCost = value;
            OnPropertyChanged(nameof(priceCost));
        }
    }

    private string _description;

    public string description
    {
        get { return _description; }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(description));
        }
    }

    private ObservableCollection<ArticleType> _articleTypes;

    public ObservableCollection<ArticleType> articleTypes
    {
        get { return _articleTypes; }
        set
        {
            _articleTypes = value;
            OnPropertyChanged(nameof(articleTypes));
        }
    }


    public event Action reloadItems;
    public ICommand updateArticleCommand { get; }
    public ICommand exitCommand { get; }
    private UpdateItemWindow _window;

    public UpdateArticleViewModel(IArticleTypeRepository articleTypeRepository, IArticleRepository articleRepository,
        Article article, UpdateItemWindow window)
    {
        _window = window;
        _article = article;
        _articleTypes = new ObservableCollection<ArticleType>();
        _articleRepository = articleRepository;
        _articleTypeRepository = articleTypeRepository;
        exitCommand = new AsyncRelayCommand(Exit);
        updateArticleCommand = new AsyncRelayCommand(UpdateArticle);
        LoadItem(_article);
    }

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
            foreach (var type in articleTypes)
            {
                _articleTypes.Add(type);
            }
        }
    }

    private async Task UpdateArticle()
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

        _article.ArticleId = articleId;
        _article.ArticleTypeId = articleTypeId;
        _article.Name = articleName;
        _article.PriceBuy = priceBuy;
        _article.Stock = stock;
        _article.PriceCost = priceCost;
        _article.Description = description;
        await _articleRepository.UpdateArticle(_article);
        reloadItems.Invoke();
        MessageBox.Show("Artículo actualizado correctamente");
        await Exit();
    }

    private async Task Exit()
    {
        _window.Close();
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