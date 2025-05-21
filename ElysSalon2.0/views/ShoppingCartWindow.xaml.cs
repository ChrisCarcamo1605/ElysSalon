using System.Windows;
using Application.Services;
using AutoMapper;
using Core.Interfaces.Services;
using ElysSalon2._0.ViewModels;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.views;

/// <summary>
///     Lógica de interacción para ShoppingCartWindow.xaml
/// </summary>
public partial class ShoppingCartWindow : Window
{
    private readonly IMapper _mapper;
    private readonly WindowsManager _windowsManager;

    public ShoppingCartWindow(ArticleAppService articleService, IMapper mapper, WindowsManager windowsManager,
        SaleDataAppService SaleService)
    {
        InitializeComponent();
        DataContext = new ShoppingCartViewModel(articleService, mapper, SaleService, windowsManager, this);
    }
}