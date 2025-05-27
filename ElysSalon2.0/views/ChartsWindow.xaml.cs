using System.Collections.ObjectModel;
using System.Windows;
using Application.DTOs.Request.SalesData;
using ElysSalon2._0.ViewModels;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.views;

/// <summary>
///     Lógica de interacción para Charts.xaml
/// </summary>
public partial class ChartsWindow : Window
{
    public ChartsWindow(WindowsManager windowsManager, ObservableCollection<DTOSalesData> salesCollection,
        ObservableCollection<DTOSalesData> ticketCollection, ObservableCollection<DTOSalesData> expensesCollection,
        ObservableCollection<DTOSalesData> tdetailsCollection)
    {
        InitializeComponent();

        DataContext = new ChartsViewModel(this, windowsManager, salesCollection, ticketCollection, expensesCollection,
            tdetailsCollection);
    }

    private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
    {
    }
}