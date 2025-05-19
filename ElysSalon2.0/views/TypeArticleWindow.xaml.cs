using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.views;

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