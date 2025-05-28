using System.Windows;
using Application.Services;
using ElysSalon2._0.ViewModels;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.views;

/// <summary>
///     Lógica de interacción para ItemManagerWindow.xaml
/// </summary>
public partial class ItemManagerWindow : Window
{
    public ItemManagerWindow(ArticleAppService service, WindowsManager windowsManager)
    {
        InitializeComponent();
        DataContext = new ItemManagerViewModel(this, service, windowsManager);
    }

    private void typeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }
}