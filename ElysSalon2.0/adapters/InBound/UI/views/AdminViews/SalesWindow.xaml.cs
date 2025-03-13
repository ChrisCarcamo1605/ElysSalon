using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.ViewModels;
using ElysSalon2._0.Core.aplication.Management;
using ElysSalon2._0.Core.aplication.Ports.Repositories;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

/// <summary>
///     Lógica de interacción para SalesWindow.xaml
/// </summary>
public partial class SalesWindow : Window
{
    public SalesWindow(ISalesRepository sales, WindowsManager _winManager)
    {
        InitializeComponent();

        DataContext = new SalesViewModel(sales, this, _winManager);
    }
}