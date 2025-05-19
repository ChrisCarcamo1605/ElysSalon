using System.Windows;
using ElysSalon2._0.WinManagement;

namespace ElysSalon2._0.views;

/// <summary>
///     Lógica de interacción para ItemManagerWindow.xaml
/// </summary>
public partial class ItemManagerWindow : Window
{
    public ItemManagerWindow(IArticleRepository articleRepository, IArticleTypeRepository TypeRepository,
        IArticleService service, WindowsManager windowsManager)
    {
        InitializeComponent();
        DataContext = new ItemManagerViewModel(articleRepository, TypeRepository, this, service, windowsManager);
    }
}