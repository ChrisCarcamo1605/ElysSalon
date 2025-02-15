﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows.UI.WindowManagement.Preview;
using ElysSalon2._0.aplication.DTOs.ArticleType;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews {
    public partial class TypeArticleWindow : Window, INotifyPropertyChanged, IChildWindow
    {
        private IArticleTypeRepository _typeRepository;
        private DTOGetTypeArticles selectItem;
        private ItemManager _itemsManager;
        private WindowsManager _windowManagement;
        public event Action? UpdateParentGrid;
        public ObservableCollection<DTOGetTypeArticles> typesCollection
        {
            get { return _typesCollection; }
            set
            {
                _typesCollection = value;
                OnPropertyChanged(nameof(typesCollection));
            }
        }

        private ICollectionView _view;
        private ObservableCollection<DTOGetTypeArticles> _typesCollection;


        public TypeArticleWindow(IArticleTypeRepository typeRepository, WindowsManager windowManagement, ItemManager itemsManager){
            _windowManagement = windowManagement;
            _itemsManager = itemsManager;
            InitializeComponent();
            _typeRepository = typeRepository;
            DataContext = this;
            loadItems();
        }

        public void loadItems(){
            var types = _typeRepository.getTypes();

            if (_typesCollection == null)
            {
                _typesCollection = new ObservableCollection<DTOGetTypeArticles>(types);
                _view = CollectionViewSource.GetDefaultView(typesCollection);
                typeGrid.ItemsSource = _view;
            }
            else
            {
                typesCollection.Clear();
                foreach (var type in types)
                {
                    _typesCollection.Add(type);
                }
            }
        }

        private void typeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e){
        }

        private void nameTypeTxt_Loaded(object sender, RoutedEventArgs e){
            UIElementsUtil.lostFocus("Nombre...", nameTypeTxt);
        }

        private void nameTypeTxt_LostFocus(object sender, RoutedEventArgs e){
            UIElementsUtil.lostFocus("Nombre...", nameTypeTxt);
        }

        private void nameTypeTxt_GotFocus(object sender, RoutedEventArgs e){
            UIElementsUtil.gotFocus("Nombre...", nameTypeTxt);
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

        private void deleteTypeBtn_Click(object sender, RoutedEventArgs e){
            var index = typeGrid.SelectedIndex;
            if (index != -1)
            {
                var type = typesCollection[index];
                MessageBoxResult result = MessageBox.Show($"¿Está seguro de eliminar este item? {type.article_type}",
                    "Eliminar",
                    MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    _typeRepository.deleteType(type.typeId);
                    loadItems();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un item primero");
            }
        }

        private void updateTypeBtn_Click(object sender, RoutedEventArgs e){
            var index = typeGrid.SelectedIndex;
            var selectedItem = typesCollection[index];

            var type = (new DTOGetArticleType(selectedItem.typeId, selectedItem.article_type));

            if (index != -1)
            {
                _typeRepository.updateType(type);
                MessageBox.Show("Item actualizado");
                loadItems();
            }

            else
            {
                MessageBox.Show("Seleccione un item primero");
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e){
            
            UpdateParentGrid?.Invoke();
            this.Close();
        }

        
    }
}