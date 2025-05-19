using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.views;

public partial class MainWindow : Window
{
    private readonly IServiceProvider _serviceProvider;
    private readonly WindowsManager _windowsManager;

    public ICommand ServiceClickCommand { get; }
    public ICommand AdminClickCommand { get; }

    public MainWindow(IServiceProvider serviceProvider, WindowsManager windows)
    {
        _windowsManager = windows;
        _serviceProvider = serviceProvider;
        InitializeComponent();
        DataContext = this;
        AdminClickCommand = new RelayCommand(adminBtnClick);
        ServiceClickCommand = new RelayCommand(ProductsClick);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
    }

    private void adminBtnClick()
    {
        _windowsManager.CloseCurrentWindowandShowWindow<AdminWindow>(this);
    }

    private void ProductsClick()
    {
        _windowsManager.CloseCurrentWindowandShowWindow<ShoppingCartWindow>(this);
    }

    private void AdministradorClick()
    {
    }
}