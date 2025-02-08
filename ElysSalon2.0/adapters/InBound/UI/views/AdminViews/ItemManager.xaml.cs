using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Windows.Devices.PointOfService;
using ElysSalon2._0.adapters.OutBound;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

/// <summary>
/// Lógica de interacción para ItemManager.xaml
/// </summary>
public partial class ItemManager : Window, INotifyPropertyChanged {
    private readonly IArticleRepository _articleRepository;

    public ObservableCollection<DTOGetArticles> articlesCollection
    {
        get { return _articlesCollection; }
        set
        {
            _articlesCollection = value;
            OnPropertyChanged(nameof(articlesCollection));
        }
    }

    private DTOAddArticle dto;
    private WindowsManager _windowsManager;

    public ItemManager(IServiceProvider serviceProvider, WindowsManager windowsManager,
        IArticleRepository articleRepository){
        InitializeComponent();
        _articleRepository = articleRepository;
        _windowsManager = windowsManager;
        DataContext = this;
        LoadItems();
    }

    private ObservableCollection<DTOGetArticles> _articlesCollection;


    private void LoadItems(){
        var articles = _articleRepository.GetArticles();

        if (_articlesCollection == null)
        {
            _articlesCollection = new ObservableCollection<DTOGetArticles>(articles);
        }
        else
        {
            _articlesCollection.Clear();
            foreach (var article in articles)
            {
                _articlesCollection.Add(article); // Agrega los nuevos datos
            }
        }

        // Aquí, llamas a OnPropertyChanged para asegurarte de que los cambios se notifiquen
        OnPropertyChanged(nameof(articlesCollection));
    }


    private void exitBtn_Click(object sender, RoutedEventArgs e){
        _windowsManager.closeCurrentWindowandShowWindow<AdminWindow>(this);
    }


    private void TextBox_GotFocus(object sender, RoutedEventArgs e){
        var textBox = nameTxtBox;
        if (textBox.Text == "Nombre Item")
        {
            textBox.Text = "";
            textBox.Foreground = new SolidColorBrush(Colors.Black);
        }
    }

    private void nameTxtBox_LostFocus(object sender, RoutedEventArgs e){
        var textBox = nameTxtBox;

        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "Nombre Item";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void nameTxtBox_Loaded_1(object sender, RoutedEventArgs e){
        var textBox = nameTxtBox;

        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "Nombre Item";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void priceCostBox_Loaded(object sender, RoutedEventArgs e){
        var textBox = priceCostBox;
        if (string.IsNullOrWhiteSpace(priceCostBox.Text))
        {
            textBox.Text = "Precio Costo";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void priceCostBox_LostFocus(object sender, RoutedEventArgs e){
        var textBox = priceCostBox;
        if (string.IsNullOrWhiteSpace(priceCostBox.Text))
        {
            textBox.Text = "Precio Costo";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void priceCostBox_GotFocus(object sender, RoutedEventArgs e){
        var textBox = priceCostBox;

        if (textBox.Text == "Precio Costo")
        {
            textBox.Text = "";
            textBox.Foreground = new SolidColorBrush(Colors.Black);
        }
    }

    private void priceBuyBox_GotFocus(object sender, RoutedEventArgs e){
        var textBox = priceBuyBox;

        if (textBox.Text == "Precio Venta")
        {
            textBox.Text = "";
            textBox.Foreground = new SolidColorBrush(Colors.Black);
        }
    }

    private void priceBuyBox_LostFocus(object sender, RoutedEventArgs e){
        var textBox = priceBuyBox;

        if (string.IsNullOrWhiteSpace(priceBuyBox.Text))
        {
            textBox.Text = "Precio Venta";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void priceBuyBox_Loaded(object sender, RoutedEventArgs e){
        var textBox = priceBuyBox;

        if (string.IsNullOrWhiteSpace(priceBuyBox.Text))
        {
            textBox.Text = "Precio Venta";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void descriptionBox_GotFocus(object sender, RoutedEventArgs e){
        var textBox = descriptionBox;

        if (textBox.Text == "Descripcion")
        {
            textBox.Text = "";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void descriptionBox_LostFocus(object sender, RoutedEventArgs e){
        var textBox = descriptionBox;


        if (string.IsNullOrWhiteSpace(descriptionBox.Text))
        {
            textBox.Text = "Descripcion";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void descriptionBox_Loaded(object sender, RoutedEventArgs e){
        var textBox = descriptionBox;

        if (string.IsNullOrWhiteSpace(descriptionBox.Text))
        {
            textBox.Text = "Descripcion";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }


    private void addArticleBtn_Click(object sender, RoutedEventArgs e){
        DTOAddArticle dto = new(
            nameTxtBox.Text,
            typeComboBox.Text,
            decimal.Parse(priceCostBox.Text),
            decimal.Parse(priceBuyBox.Text),
            int.Parse(stockBox.Text),
            descriptionBox.Text
        );

        _articleRepository.AddArticle(dto);

        MessageBox.Show("Articulo Agregado con exito!");
        LoadItems();
        nameTxtBox.Clear();
        typeComboBox.Text = "Tipo Articulo";
        priceCostBox.Clear();
        stockBox.Clear();
        descriptionBox.Clear();
        priceBuyBox.Clear();
        articlesGrid.Items.Refresh();
    }

    private void typeComboBox_Loaded(object sender, RoutedEventArgs e){
        var textBox = typeComboBox;


        IArticleRepository articleRepository = new ArticleRepository();
        var types = articleRepository.getTypeArticle();
        foreach (var i in types) typeComboBox.Items.Add(i.name);

        if (textBox.Text.Equals("Tipo Articulo")) textBox.Foreground = new SolidColorBrush(Colors.Gray);

        ;
    }

    private void stockBox_Loaded(object sender, RoutedEventArgs e){
        var textBox = stockBox;

        if (string.IsNullOrWhiteSpace(stockBox.Text))
        {
            textBox.Text = "Stock";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void stockBox_LostFocus(object sender, RoutedEventArgs e){
        var textBox = stockBox;


        if (string.IsNullOrWhiteSpace(stockBox.Text))
        {
            textBox.Text = "Stock";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void stockBox_GotFocus(object sender, RoutedEventArgs e){
        var textBox = stockBox;

        if (textBox.Text == "Stock")
        {
            textBox.Text = "";
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    private void typeComboBox_LostFocus(object sender, RoutedEventArgs e){
        var textBox = typeComboBox;

        if (textBox.Text.Equals("Tipo Articulo")) textBox.Foreground = new SolidColorBrush(Colors.Gray);
    }

    private void typeComboBox_GotFocus(object sender, RoutedEventArgs e){
        var textBox = typeComboBox;


        if (!textBox.Text.Equals("Tipo Articulo")) textBox.Foreground = new SolidColorBrush(Colors.Black);
    }


    private void ItemGrid_Loaded(object sender, RoutedEventArgs e){
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null){
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null){
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}