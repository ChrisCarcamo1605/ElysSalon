using System.Collections.ObjectModel;
using System.Windows;
using ElysSalon2._0.adapters.InBound.ViewModels;
using ElysSalon2._0.aplication.DTOs.Request.SalesData;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.domain.Entities;

namespace ElysSalon2._0.adapters.InBound.views;

/// <summary>
/// Lógica de interacción para Charts.xaml
/// </summary>
public partial class ChartsWindow : Window
{
    public ChartsWindow(WindowsManager windowsManager, ObservableCollection<DTOSalesData> salesCollection,
        ObservableCollection<DTOSalesData> ticketCollection, ObservableCollection<DTOSalesData> expensesCollection,
        ObservableCollection<TicketDetails> tdetailsCollection)
    {
        InitializeComponent();

        DataContext = new ChartsViewModel(this, windowsManager, salesCollection, ticketCollection, expensesCollection,
            tdetailsCollection);
    }

    private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
    {
    }
}