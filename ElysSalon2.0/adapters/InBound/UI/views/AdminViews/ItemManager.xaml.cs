﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using ElysSalon2._0.aplication.DTOs;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

public partial class ItemManager : Window, INotifyPropertyChanged {
    private readonly IArticleRepository _articleRepository;
    private DTOGetArticles _selectedArticle;

    private ICollectionView _articlesView;

    public ObservableCollection<DTOGetArticles> articlesCollection
    {
        get { return _articlesCollection; }
        set
        {
            _articlesCollection = value;
            OnPropertyChanged(nameof(articlesCollection));
        }
    }


    public DTOGetArticles SelectedArticle
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

        int selectedIndex = itemGrid.SelectedIndex;

        if (_articlesCollection == null)
        {
            _articlesCollection = new ObservableCollection<DTOGetArticles>(articles);
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

        _articlesView.Refresh();
    }


    private void exitBtn_Click(object sender, RoutedEventArgs e){
        _windowsManager.closeCurrentWindowandShowWindow<AdminWindow>(this);
    }


    private void TextBox_GotFocus(object sender, RoutedEventArgs e){
        gotFocus("Nombre Item", nameTxtBox);
    }

    private void nameTxtBox_LostFocus(object sender, RoutedEventArgs e){
        lostFocus("Nombre Item", nameTxtBox);
    }

    private void nameTxtBox_Loaded_1(object sender, RoutedEventArgs e){
        gotFocus("Nombre Item", nameTxtBox);
    }

    private void priceCostBox_Loaded(object sender, RoutedEventArgs e){
        gotFocus("Precio Costo", priceCostBox);
    }

    private void priceCostBox_LostFocus(object sender, RoutedEventArgs e){
        lostFocus("Precio Costo", priceCostBox);
    }

    private void priceCostBox_GotFocus(object sender, RoutedEventArgs e){
        gotFocus("Precio Costo", priceCostBox);
    }

    private void priceBuyBox_GotFocus(object sender, RoutedEventArgs e){
        gotFocus("Precio Venta", priceBuyBox);
    }

    private void priceBuyBox_LostFocus(object sender, RoutedEventArgs e){
        lostFocus("Precio Venta", priceBuyBox);
    }

    private void priceBuyBox_Loaded(object sender, RoutedEventArgs e){
        gotFocus("Precio Venta", priceBuyBox);
    }

    private void descriptionBox_GotFocus(object sender, RoutedEventArgs e){
        gotFocus("Descripcion", descriptionBox);
    }

    private void descriptionBox_LostFocus(object sender, RoutedEventArgs e){
        lostFocus("Descripcion", descriptionBox);
    }

    private void descriptionBox_Loaded(object sender, RoutedEventArgs e){
        gotFocus("Descripcion", descriptionBox);
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
        priceBuyBox.Clear();
        stockBox.Clear();
        descriptionBox.Clear();
        lostFocus("Nombre Item", nameTxtBox);
        lostFocus("Tipo Item", typeComboBox);
        lostFocus("Precio Costo", priceCostBox);
        lostFocus("Stock", stockBox);
        lostFocus("Precio Venta", priceBuyBox);
        lostFocus("Descripcion", descriptionBox);
    }

    private void typeComboBox_Loaded(object sender, RoutedEventArgs e){
        var textBox = typeComboBox;

        var types = _articleRepository.getListTypeArticle();
        foreach (var i in types) typeComboBox.Items.Add(i.name);

        if (textBox.Text.Equals("Tipo Articulo")) textBox.Foreground = new SolidColorBrush(Colors.Gray);
    }

    private void stockBox_Loaded(object sender, RoutedEventArgs e){
        gotFocus("Stock", stockBox);
    }

    private void stockBox_LostFocus(object sender, RoutedEventArgs e){
        lostFocus("Stock", stockBox);
    }

    private void stockBox_GotFocus(object sender, RoutedEventArgs e){
        gotFocus("Stock", stockBox);
    }

    private void typeComboBox_LostFocus(object sender, RoutedEventArgs e){
        lostFocus("Tipo Articulo", typeComboBox);
    }

    private void typeComboBox_GotFocus(object sender, RoutedEventArgs e){
        if (!typeComboBox.Text.Equals("Tipo Articulo")) typeComboBox.Foreground = new SolidColorBrush(Colors.Black);
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null){
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void gotFocus(string text, dynamic textBox){
        if (!string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = "";
            textBox.Foreground = new SolidColorBrush(Colors.Black);
        }
        else
        {
            textBox.Text = text;
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }

    public void lostFocus(string text, dynamic textBox){
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.Text = text;
            textBox.Foreground = new SolidColorBrush(Colors.Gray);
        }
    }


    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null){
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    private void updateBtn_Click(object sender, RoutedEventArgs e){
        var selectedIndex = itemGrid.SelectedIndex;
        if (selectedIndex == -1)
        {
            MessageBox.Show("Por favor, seleccione un artículo para actualizar.");
            return;
        }

        var selectedItem = (DTOGetArticles)itemGrid.Items[selectedIndex];


        var item = new Article(new DTOUpdateArticle(
            selectedItem.article_id,
            selectedItem.article_name,
            _articleRepository.getArticleTypeId(selectedItem.article_type),
            selectedItem.price_cost,
            selectedItem.price_buy,
            selectedItem.stock,
            selectedItem.description));

        try
        {
            _articleRepository.UpdateArticle(item);
            MessageBox.Show("Artículo actualizado exitosamente");
            var currentArticleId = selectedItem.article_id;
            LoadItems();
            for (int i = 0; i < itemGrid.Items.Count; i++)
            {
                var article = (DTOGetArticles)itemGrid.Items[i];
                if (article.article_id == currentArticleId)
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

    private void deleteBtn_Click(object sender, RoutedEventArgs e){
        if (SelectedArticle == null)
        {
            MessageBox.Show("Por favor, seleccione un artículo para eliminar.");
            return;
        }

        var result = MessageBox.Show(
            $"¿Está seguro que desea eliminar el artículo '{SelectedArticle.article_name}'?",
            "Confirmar eliminación",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            _articleRepository.DeleteArticle(SelectedArticle.article_id);

            MessageBox.Show("Artículo eliminado exitosamente");
            LoadItems();
        }
    }

    private void itemGrid_SelectionChanged(object sender, SelectionChangedEventArgs e){
        if (itemGrid.SelectedItem is DTOGetArticles article)
        {
            SelectedArticle = article;
        }
    }

    private void itemGrid_KeyDown(object sender, KeyEventArgs e){
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

                SelectedArticle = nextItem as DTOGetArticles;
            }
        }
    }

    private void itemGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e){
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

    private void sortComboBox_Loaded(object sender, RoutedEventArgs e){
        var types = _articleRepository.getListTypeArticle();
        sortComboBox.Items.Add("Todo");
        sortComboBox.SelectedIndex = 0;
        foreach (var i in types) sortComboBox.Items.Add(i.name);

        if (sortComboBox.Text.Equals("Todo")) sortComboBox.Foreground = new SolidColorBrush(Colors.Gray);
    }

    private void sortComboBox_GotFocus(object sender, RoutedEventArgs e){
        if (!sortComboBox.Text.Equals("Todo")) sortComboBox.Foreground = new SolidColorBrush(Colors.Black);
    }

    private void sortComboBox_LostFocus(object sender, RoutedEventArgs e){
        lostFocus("Todo", sortComboBox);
    }

    private void sortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e){
        if (sortComboBox.SelectedItem is string itemSelected && sortComboBox.IsMouseCaptured)
        {
            if (!itemSelected.Equals("Todo"))
            {
                _articlesView.Filter = (o) =>
                {
                    var article = (DTOGetArticles)o;
                    return article.article_type.Equals(itemSelected);
                };
            }
            else
            {
                _articlesView.Filter = null;
            }

            _articlesView.Refresh();
        }
    }

    private void searchTxtBox_TextChanged(object sender, TextChangedEventArgs e){
        string searchText = searchTxtBox.Text.ToLower();

        if (string.IsNullOrWhiteSpace(searchText) || searchText.Equals("nombre..."))
        {
            _articlesView.Filter = null;
        }
        else
        {
            _articlesView.Filter = (obj) =>
            {
                if (obj is DTOGetArticles article)
                {
                    return article.article_name.ToLower().Contains(searchText);
                }

                return false;
            };
        }

        _articlesView.Refresh();
    }

    private void searchTxtBox_Loaded(object sender, RoutedEventArgs e){
        lostFocus("Nombre...", searchTxtBox);
    }

    private void searchTxtBox_LostFocus(object sender, RoutedEventArgs e){
        lostFocus("Nombre...", searchTxtBox);
    }

    private void searchTxtBox_GotFocus(object sender, RoutedEventArgs e){
        gotFocus("Nombre...", searchTxtBox);
    }

    public void onlyDigits(dynamic textBox, TextCompositionEventArgs e)
    {
        char caracter = e.Text[0];

        if (!char.IsDigit(caracter) && !char.IsControl(caracter) && caracter != '.')
        {
            e.Handled = true;
        }
        else if (caracter == '.' && textBox.Text.Contains('.')) // Evitar múltiples puntos
        {
            e.Handled = true;
        }
    }


    private void nameTxtBox_PreviewTextInput(object sender, TextCompositionEventArgs e){
       
    }

    private void stockBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        onlyDigits(stockBox, e);
    }

    private void priceBuyBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        onlyDigits(priceBuyBox, e);
    }

    private void priceCostBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        onlyDigits(priceCostBox, e);
    }
}