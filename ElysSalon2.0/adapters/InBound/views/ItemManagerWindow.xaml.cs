using System.Windows;
using ElysSalon2._0.adapters.InBound.ViewModels;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;

namespace ElysSalon2._0.adapters.InBound.views;

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