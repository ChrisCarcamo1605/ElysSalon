using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.ViewModels;
using ElysSalon2._0.Core.aplication.Management;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.aplication.Ports.Services;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

/// <summary>
///     Lógica de interacción para ItemManagerWindow.xaml
/// </summary>
public partial class ItemManagerWindow : Window
{
    public ItemManagerWindow(IArticleRepository articleRepository, IArticleTypeRepository TypeRepository,
        IArticleService service, WindowsManager windowsManager)
    {
        InitializeComponent();
        DataContext = new ItemManagerViewModel(articleRepository, TypeRepository, this, service, windowsManager);
    }
}