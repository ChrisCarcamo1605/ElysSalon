﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

public partial class TypeArticleWindow : Window, INotifyPropertyChanged, IChildWindow
{
    private readonly IArticleTypeRepository _typeRepository;
    private ItemManager _itemsManager;
    private ObservableCollection<ArticleType> _typesCollection;

    private ICollectionView _view;
    private WindowsManager _windowManagement;


    public TypeArticleWindow(IArticleTypeRepository typeRepository, WindowsManager windowManagement,
        ItemManager itemsManager)
    {
        _windowManagement = windowManagement;
        _itemsManager = itemsManager;
        InitializeComponent();
        _typeRepository = typeRepository;
        DataContext = this;
        loadItems();
    }

    public ObservableCollection<ArticleType> typesCollection
    {
        get => _typesCollection;
        set
        {
            _typesCollection = value;
            OnPropertyChanged();
        }
    }

    public event Action? UpdateParentGrid;

    public event PropertyChangedEventHandler? PropertyChanged;

    public void loadItems()
    {
        var types = _typeRepository.getTypes();

        if (_typesCollection == null)
        {
            _typesCollection = new ObservableCollection<ArticleType>(types.Result);
            _view = CollectionViewSource.GetDefaultView(typesCollection);
            typeGrid.ItemsSource = _view;
        }
        else
        {
            typesCollection.Clear();
            foreach (var type in types.Result) _typesCollection.Add(type);
        }
    }

    private void typeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
    }

    private void nameTypeTxt_Loaded(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Nombre...", nameTypeTxt);
    }

    private void nameTypeTxt_LostFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.lostFocus("Nombre...", nameTypeTxt);
    }

    private void nameTypeTxt_GotFocus(object sender, RoutedEventArgs e)
    {
        UIElementsUtil.TextBoxGotFocus(sender, e);
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

    private void deleteTypeBtn_Click(object sender, RoutedEventArgs e)
    {
        var index = typeGrid.SelectedIndex;
        if (index != -1)
        {
            var type = typesCollection[index];
            var result = MessageBox.Show($"¿Está seguro de eliminar este item? {type.Name}",
                "Eliminar",
                MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _typeRepository.deleteType(type.ArticleTypeId);
                loadItems();
            }
        }
        else
        {
            MessageBox.Show("Seleccione un item primero");
        }
    }

    private void updateTypeBtn_Click(object sender, RoutedEventArgs e)
    {
        //
    }

    private void exitBtn_Click(object sender, RoutedEventArgs e)
    {
        UpdateParentGrid?.Invoke();
        Close();
    }

    private void addTypeBtn_Click(object sender, RoutedEventArgs e)
    {
        var nombre = nameTypeTxt.Text;

        _typeRepository.addType(nombre);
        MessageBox.Show("Tipo agregado con exito!");
        loadItems();
    }
}