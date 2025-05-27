using System.Windows;
using Application.Services;
using AutoMapper;
using ElysSalon2._0.ViewModels;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.views;

/// <summary>
///     Interaction logic for SalesWindow.xaml
/// </summary>
public partial class SalesWindow : Window
{
    public SalesWindow(WindowsManager winManager, SaleDataAppService salesDataService,
        SaleReportsService reportsService, IMapper mapper)

    {
        InitializeComponent();
        DataContext = new SalesViewModel(this, winManager, salesDataService, reportsService, mapper);
    }
}