using System.Windows;
using AutoMapper;
using ElysSalon2._0.adapters.InBound.ViewModels;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;

namespace ElysSalon2._0.adapters.InBound.views;

/// <summary>
/// Interaction logic for SalesWindow.xaml
/// </summary>
public partial class SalesWindow : Window
{
    public SalesWindow(WindowsManager winManager, ITicketService ticketRepo, ISalesService service, IMapper mapper)

    {
        InitializeComponent();
        DataContext = new SalesViewModel(this, winManager, ticketRepo, service, mapper);
    }
}