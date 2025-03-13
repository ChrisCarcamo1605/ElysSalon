using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.ViewModels;

namespace ElysSalon2._0.adapters.InBound.UI.views.MainViews;

/// <summary>
///     Lógica de interacción para confirmWindow.xaml
/// </summary>
public partial class ConfirmWindow : Window
{
    public ConfirmWindow(ShoppingCartViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void siBtn_Click(object sender, RoutedEventArgs e)
    {
    }

    private void noBtn_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}