using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.aplication.DTOs.DTOArticle;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Mappers;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

public partial class ItemManager : Window, INotifyPropertyChanged
{
    private readonly IArticleRepository _articleRepository;
    private ObservableCollection<string> _typeCollections;
    private IMapper _articleMap;
    private DTOGetArticleGrid _selectedArticle;
    public ICollectionView _articlesView;
    private IArticleTypeRepository _typeRepository;
    private WindowsManager _windowsManager;

    public ObservableCollection<string> typeCollections
    {
        get { return _typeCollections; }
        set
        {
            _typeCollections = value;
            OnPropertyChanged(nameof(typeCollections));
        }
    }

    public ObservableCollection<Article> articlesCollection
    {
        get { return _articlesCollection; }
        set
        {
            _articlesCollection = value;
            OnPropertyChanged(nameof(articlesCollection));
        }
    }

    public DTOGetArticleGrid SelectedArticle
    {
        get => _selectedArticle;
        set
        {
            if (_selectedArticle != value)
            {
                _selectedArticle = value;
                OnPropertyChanged(nameof(SelectedArticle));
            }
        }
    }

    public ItemManager(IServiceProvider serviceProvider, WindowsManager windowsManager,
        IArticleRepository articleRepository, IArticleTypeRepository TypeRepository, IMapper articleMapping)
    {


        InitializeComponent();
        _articleMap = articleMapping;
        _typeRepository = TypeRepository;
        _articleRepository = articleRepository;
        _windowsManager = windowsManager;
        _windowsManager.GridUpdateRequested += LoadItems;
        typeCollections = new ObservableCollection<string>();
        DataContext = this;
        LoadItems();
    }

    private ObservableCollection<Article> _articlesCollection;


    public void LoadItems()
    {
        var articles = _articleRepository.GetArticles();
        var types = _typeRepository.getTypes();

        if (_typeCollections == null)
        {
            _typeCollections = new ObservableCollection<string>();
        }
        else
        {
            _typeCollections.Clear();
            foreach (var type in types)
            {
                _typeCollections.Add(type.ArticleTypeName);
            }
        }

        if (_articlesCollection == null)
        {
            _articlesCollection = new ObservableCollection<Article>(articles);
            _articlesView = CollectionViewSource.GetDefaultView(_articlesCollection);
            itemGrid.ItemsSource = _articlesView;
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


    private void exitBtn_Click(object sender, RoutedEventArgs e)
    {
        _windowsManager.CloseCurrentWindowandShowWindow<AdminWindow>(this);
    }


    private void TextBox_GotFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Nombre Item", nameTxtBox);
    }

    private void nameTxtBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Nombre Item", nameTxtBox);
    }

    private void nameTxtBox_Loaded_1(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Nombre Item", nameTxtBox);
    }

    private void priceCostBox_Loaded(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Precio Costo", priceCostBox);
    }

    private void priceCostBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Precio Costo", priceCostBox);
    }

    private void priceCostBox_GotFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Precio Costo", priceCostBox);
    }

    private void priceBuyBox_GotFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Precio Venta", priceBuyBox);
    }

    private void priceBuyBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Precio Venta", priceBuyBox);
    }

    private void priceBuyBox_Loaded(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Precio Venta", priceBuyBox);
    }

    private void descriptionBox_GotFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Descripcion", descriptionBox);
    }

    private void descriptionBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Descripcion", descriptionBox);
    }

    private void descriptionBox_Loaded(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Descripcion", descriptionBox);
    }


    private void addArticleBtn_Click(object sender, RoutedEventArgs e)
    {
        if (priceCostBox.Text.Equals("Precio Costo") || priceBuyBox.Text.Equals("Precio Venta") ||
            typeComboBox.Text.Equals("Tipo Articulo"))
        {
            MessageBox.Show("Porfavor llene los campos vacíos");
        }
        else
        {
            int typeId = _typeRepository.getTypeId(typeComboBox.Text);
            var newArticle =new DTOAddArticle(
                nameTxtBox.Text, 
                _typeRepository.getArticleType(typeId),
                decimal.Parse(priceCostBox.Text),
                decimal.Parse(priceBuyBox.Text),
                int.Parse(stockBox.Text),
                descriptionBox.Text);

            _articleRepository.AddArticle(_articleMap.Map<Article>(newArticle));

            MessageBox.Show("Articulo Agregado con exito!");
            LoadItems();
            nameTxtBox.Clear();
            typeComboBox.Text = "Tipo Articulo";
            priceCostBox.Clear();
            priceBuyBox.Clear();
            stockBox.Clear();
            descriptionBox.Clear();
            UIElementsUtil.lostFocus("Nombre Item", nameTxtBox);
            UIElementsUtil.lostFocus("Tipo Item", typeComboBox);
            UIElementsUtil.lostFocus("Precio Costo", priceCostBox);
            UIElementsUtil.lostFocus("Stock", stockBox);
            UIElementsUtil.lostFocus("Precio Venta", priceBuyBox);
            UIElementsUtil.lostFocus("Descripcion", descriptionBox);
        }
    }

    private void typeComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        var textBox = typeComboBox;

        var types = _typeRepository.getTypes();
        foreach (var i in types) typeComboBox.Items.Add(i.ArticleTypeName);

        if (textBox.Text.Equals("Tipo Articulo")) textBox.Foreground = new SolidColorBrush(Colors.Gray);
    }

    private void stockBox_Loaded(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Stock", stockBox);
    }

    private void stockBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Stock", stockBox);
    }

    private void stockBox_GotFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Stock", stockBox);
    }

    private void typeComboBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Tipo Articulo", typeComboBox);
    }

    private void typeComboBox_GotFocus(object sender, RoutedEventArgs e)
    {
        if (!typeComboBox.Text.Equals("Tipo Articulo")) typeComboBox.Foreground = new SolidColorBrush(Colors.Black);
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

    private void updateBtn_Click(object sender, RoutedEventArgs e)
    {
        var selectedIndex = itemGrid.SelectedIndex;
        if (selectedIndex == -1)
        {
            MessageBox.Show("Por favor, seleccione un artículo para actualizar.");
            return;
        }

        var selectedItem = (Article)itemGrid.Items[selectedIndex];


        var item = (new DTOUpdateArticle(
            selectedItem.articleId,
            selectedItem.articleName,
            _typeRepository.getArticleType(selectedItem.articleTypeId),
            selectedItem.priceCost,
            selectedItem.priceBuy,
            selectedItem.stock,
            selectedItem.description));

        try
        {
            _articleRepository.UpdateArticle(_articleMap.Map<Article>(item));
            MessageBox.Show("Artículo actualizado exitosamente");
            var currentArticleId = selectedItem.articleId;
            LoadItems();
            for (int i = 0; i < itemGrid.Items.Count; i++)
            {
                var article = (DTOUpdateArticle)itemGrid.Items[i];
                if (article.articleId == currentArticleId)
                {
                    itemGrid.SelectedIndex = i;
                    itemGrid.ScrollIntoView(itemGrid.SelectedItem);
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al actualizar: {ex.Message}");
        }
    }

    private void deleteBtn_Click(object sender, RoutedEventArgs e)
    {
        if (SelectedArticle == null)
        {
            MessageBox.Show("Por favor, seleccione un artículo para eliminar.");
            return;
        }

        var result = MessageBox.Show(
            $"¿Está seguro que desea eliminar el artículo '{SelectedArticle.articleName}'?",
            "Confirmar eliminación",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            _articleRepository.DeleteArticle(SelectedArticle.articleId);

            MessageBox.Show("Artículo eliminado exitosamente");
            LoadItems();
        }
    }

    private void itemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (itemGrid.SelectedItem is DTOGetArticleGrid article)
        {
            SelectedArticle = article;
        }
    }

    private void itemGrid_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            e.Handled = true;

            var currentCell = itemGrid.CurrentCell;
            var currentItem = itemGrid.SelectedItem;

            if (currentCell.Column == null || currentItem == null) return;

            var currentRowIndex = itemGrid.Items.IndexOf(currentItem);

            if (currentRowIndex < itemGrid.Items.Count - 1)
            {
                itemGrid.CommitEdit();

                var nextItem = itemGrid.Items[currentRowIndex + 0];

                itemGrid.SelectedItem = null;

                itemGrid.SelectedItem = nextItem;
                itemGrid.CurrentCell = new DataGridCellInfo(nextItem, currentCell.Column);

                itemGrid.Focus();
                if (itemGrid.CurrentCell.Column.GetCellContent(nextItem) is FrameworkElement element)
                {
                    element.Focus();
                }

                SelectedArticle = nextItem as DTOGetArticleGrid;
            }
        }
    }

    private void itemGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        e.Handled = true;

        var currentCell = itemGrid.CurrentCell;
        var currentItem = itemGrid.SelectedItem;

        if (currentCell.Column == null || currentItem == null) return;

        var currentRowIndex = itemGrid.Items.IndexOf(currentItem);

        if (currentRowIndex < itemGrid.Items.Count - 1)
        {
            itemGrid.CommitEdit();
        }
    }

    private void sortComboBox_Loaded(object sender, RoutedEventArgs e)
    {
        var types = _typeRepository.getTypes();
        sortComboBox.Items.Add("Todo");
        sortComboBox.SelectedIndex = 0;
        foreach (var i in types) sortComboBox.Items.Add(i.ArticleTypeName);

        if (sortComboBox.Text.Equals("Todo")) sortComboBox.Foreground = new SolidColorBrush(Colors.Gray);
    }

    private void sortComboBox_GotFocus(object sender, RoutedEventArgs e)
    {
        if (!sortComboBox.Text.Equals("Todo")) sortComboBox.Foreground = new SolidColorBrush(Colors.Black);
    }

    private void sortComboBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Todo", sortComboBox);
    }

    private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sortComboBox.SelectedItem is string itemSelected && sortComboBox.IsMouseCaptured)
        {
            if (!itemSelected.Equals("Todo"))
            {
                _articlesView.Filter = (o) =>
                {
                    var article = (DTOGetArticleGrid)o;
                    return article.articleType.Equals(itemSelected);
                };
            }
            else
            {
                _articlesView.Filter = null;
            }

            _articlesView.Refresh();
        }
    }

    private void searchTxtBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        UIElementsUtil.searchInGrid(e, searchTxtBox, _articlesView);
    }

    private void searchTxtBox_Loaded(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Nombre...", searchTxtBox);
    }

    private void searchTxtBox_LostFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Nombre...", searchTxtBox);
    }

    private void searchTxtBox_GotFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.gotFocus("Nombre...", searchTxtBox);
    }

    private void nameTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
    }

    private void stockBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        UIElementsUtil.onlyDigits(stockBox, e);
    }

    private void priceBuyBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        UIElementsUtil.onlyDigits(priceBuyBox, e);
    }

    private void priceCostBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        UIElementsUtil.onlyDigits(priceCostBox, e);
    }

    private void addTypeBtn_Click(object sender, RoutedEventArgs e)
    {
        _windowsManager.NavigateToWindow<TypeArticleWindow>(() => LoadItems());
    }
}