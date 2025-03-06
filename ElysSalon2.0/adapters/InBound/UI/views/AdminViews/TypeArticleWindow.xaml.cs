using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Services;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.aplication.ViewModels;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

public partial class TypeArticleWindow : Window
{
    private readonly IArticleTypeRepository _typeRepository;
    private readonly ItemManagerWindow _itemsesManagerWindow;
    private readonly ObservableCollection<ArticleType> _typesCollection;

    private readonly ICollectionView _view;
    private readonly WindowsManager _windowManagement;


    public TypeArticleWindow(IArticleTypeRepository typeRepository, WindowsManager windowManagement,
        IArticleService service)
    {
        InitializeComponent();


        DataContext = new TypesManagementViewModel(typeRepository,windowManagement,service,this);

    }
}