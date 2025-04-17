using System.Collections.ObjectModel;
using System.Windows;
using ElysSalon2._0.adapters.InBound.ViewModels;
using ElysSalon2._0.aplication.DTOs.DTOSales;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;

namespace ElysSalon2._0.adapters.InBound.views;

/// <summary>
/// Lógica de interacción para Charts.xaml
/// </summary>
public partial class ChartsWindow : Window
{
    public ChartsWindow(WindowsManager windowsManager, ObservableCollection<DtoSalesList> salesCollection,
        ObservableCollection<DtoSalesList> ticketCollection, ITicketService ticketService)
    {
        InitializeComponent();

        DataContext = new ChartsViewModel(this, windowsManager, salesCollection, ticketCollection, ticketService);
    }

    private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
    {

    }
}