using System.Windows;
using AutoMapper;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.adapters.OutBound;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0.adapters.InBound.UI.views;

/// <summary>
/// Lógica de interacción para ShoppingCartWindow.xaml
/// </summary>
public partial class ShoppingCartWindow : Window {
    private readonly WindowsManager _windowsManager;
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public ShoppingCartWindow(IArticleRepository articleRepository, WindowsManager windowsManager, IMapper mapper){
        _windowsManager = windowsManager;
        _articleRepository = articleRepository;
        _mapper = mapper;
      
        InitializeComponent();
        DataContext = new ShoppingCartViewModel(articleRepository,_mapper);
    }

    private void listoBtn(object sender, RoutedEventArgs e){
    }


    private void atrasBtn_Click(object sender, RoutedEventArgs e){
        _windowsManager.CloseCurrentWindowandShowWindow<MainWindow>(this);
    }

    private void listoBtn_Click(object sender, RoutedEventArgs e){
        var confirmWindow = new ConfirmWindow();
        confirmWindow.Show();
    }
}