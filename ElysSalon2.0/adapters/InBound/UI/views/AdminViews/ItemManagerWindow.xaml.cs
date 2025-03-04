using System.Windows;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Services;
using ElysSalon2._0.aplication.ViewModels;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

public partial class ItemManager : Window
{
    public ItemManager(IArticleRepository articleRepository, IArticleTypeRepository TypeRepository,
        IArticleService service, WindowsManager windowsManager)
    {
        InitializeComponent();

        DataContext = new ItemManagerViewModel(articleRepository, TypeRepository, this, service, windowsManager);
    }
}