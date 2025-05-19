using System.Windows;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.views;

/// <summary>
///     Lógica de interacción para AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window
{
    private readonly WindowsManager _windowManager;
    private IServiceProvider _serviceProvider;

    public AdminWindow(IServiceProvider serviceProvider, WindowsManager windowsManager)
    {
        _serviceProvider = serviceProvider;
        _windowManager = windowsManager;
        InitializeComponent();
    }

    private void exitBtn_Click(object sender, RoutedEventArgs e)
    {
        _windowManager.CloseCurrentWindowandShowWindow<MainWindow>(this);
    }

    private void salesBtn_Click(object sender, RoutedEventArgs e)
    {
        _windowManager.CloseCurrentWindowandShowWindow<SalesWindow>(this);
    }

    private void itemsBtn_Click(object sender, RoutedEventArgs e)
    {
        _windowManager.CloseCurrentWindowandShowWindow<ItemManagerWindow>(this);
    }
}