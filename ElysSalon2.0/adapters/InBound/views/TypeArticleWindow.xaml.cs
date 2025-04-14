using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ElysSalon2._0.adapters.InBound.ViewModels;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.adapters.InBound.views;

public partial class TypeArticleWindow : Window
{
    private readonly ItemManagerWindow _itemsesManagerWindow;
    private readonly IArticleTypeRepository _typeRepository;
    private readonly ObservableCollection<ArticleType> _typesCollection;
    private readonly ICollectionView _view;
    private readonly WindowsManager _windowManagement;


    public TypeArticleWindow(IArticleTypeRepository typeRepository, WindowsManager windowManagement,
        IArticleService service)
    {
        InitializeComponent();


        DataContext = new TypesManagementViewModel(typeRepository, windowManagement, service, this);
    }
}