using System.Windows;
using AutoMapper;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.views;

/// <summary>
/// Interaction logic for SalesWindow.xaml
/// </summary>
public partial class SalesWindow : Window
{
    public SalesWindow(WindowsManager winManager, ISalesDataService salesDataService,
        ISalesReportsService reportsService, IMapper mapper)

    {
        InitializeComponent();
        DataContext = new SalesViewModel(this, winManager, salesDataService, reportsService, mapper);
    }
}