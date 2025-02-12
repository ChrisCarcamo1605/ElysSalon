using System.Windows;
using ElysSalon2._0.aplication.Management;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

/// <summary>
/// Lógica de interacción para AdminWindow.xaml
/// </summary>
public partial class AdminWindow : Window {

    IServiceProvider _serviceProvider;
    WindowsManager _windowManager;
    public AdminWindow(IServiceProvider serviceProvider,WindowsManager windowsManager){
        _serviceProvider = serviceProvider;
        _windowManager = windowsManager;
        InitializeComponent();
    }

    private void exitBtn_Click(object sender, RoutedEventArgs e){
        _windowManager.CloseCurrentWindowandShowWindow<MainWindow>(this);
    }

    private void salesBtn_Click(object sender, RoutedEventArgs e){
        _windowManager.CloseCurrentWindowandShowWindow<SalesWindow>(this);

    }

    private void itemsBtn_Click(object sender, RoutedEventArgs e){
        _windowManager.CloseCurrentWindowandShowWindow<ItemManager>(this);
    }
}