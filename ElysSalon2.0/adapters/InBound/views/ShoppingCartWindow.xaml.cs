using System.Windows;
using AutoMapper;
using ElysSalon2._0.adapters.InBound.ViewModels;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;

namespace ElysSalon2._0.adapters.InBound.views;

/// <summary>
///     Lógica de interacción para ShoppingCartWindow.xaml
/// </summary>
public partial class ShoppingCartWindow : Window
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly WindowsManager _windowsManager;

    public ShoppingCartWindow(IArticleService articleRepository, IMapper mapper, WindowsManager windowsManager,
        ITicketService service)
    {
        InitializeComponent();
        DataContext = new ShoppingCartViewModel(articleRepository, mapper, service, windowsManager, this);
    }
}