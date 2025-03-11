using System.Windows;
using AutoMapper;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Services;
using ElysSalon2._0.aplication.ViewModels;

namespace ElysSalon2._0.adapters.InBound.UI.views;

/// <summary>
///     Lógica de interacción para ShoppingCartWindow.xaml
/// </summary>
public partial class ShoppingCartWindow : Window
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;
    private readonly WindowsManager _windowsManager;

    public ShoppingCartWindow(IArticleRepository articleRepository, IMapper mapper, WindowsManager windowsManager,
        ITicketService service)
    {
        InitializeComponent();
        DataContext = new ShoppingCartViewModel(articleRepository, mapper, service, windowsManager, this);
    }
}