using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.adapters.InBound.UI.views.MainViews;
using ElysSalon2._0.Core.aplication.Management;

namespace ElysSalon2._0.adapters.InBound.UI.views;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;
    private readonly WindowsManager _windowsManager;

    public MainWindow(IServiceProvider serviceProvider, WindowsManager windows)
    {
        _windowsManager = windows;
        _serviceProvider = serviceProvider;
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
    }

    private void adminBtn_Click(object sender, RoutedEventArgs e)
    {
        _windowsManager.CloseCurrentWindowandShowWindow<AdminWindow>(this);
    }

    private async void btnServices_Click(object sender, RoutedEventArgs e)
    {
        _windowsManager.CloseCurrentWindowandShowWindow<ShoppingCartWindow>(this);
    }

    private void btnProducts_Click(object sender, RoutedEventArgs e)
    {
    }
}