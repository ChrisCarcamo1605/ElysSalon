using System.Windows;
using ElysSalon2._0.aplication.Management;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

/// <summary>
///     Lógica de interacción para SalesWindow.xaml
/// </summary>
public partial class SalesWindow : Window
{
    private readonly WindowsManager _windowManager;
    private IServiceProvider _serviceProvider;

    public SalesWindow(IServiceProvider serviceProvider, WindowsManager windowManager)
    {
        InitializeComponent();
        _windowManager = windowManager;
        _serviceProvider = serviceProvider;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        _windowManager.CloseCurrentWindowandShowWindow<AdminWindow>(this);
    }
}