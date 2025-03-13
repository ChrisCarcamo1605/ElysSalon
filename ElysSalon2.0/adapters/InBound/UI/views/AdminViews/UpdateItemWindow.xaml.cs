using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.ViewModels;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.aplication.Ports.Services;

namespace ElysSalon2._0.adapters.InBound.UI.views.AdminViews;

/// <summary>
///     Lógica de interacción para UpdateItemWindow.xaml
/// </summary>
public partial class UpdateItemWindow : Window
{
    public UpdateItemWindow(IArticleTypeRepository articleTypeRepository, IArticleRepository articleRepository,
        int article, IArticleService service)
    {
        InitializeComponent();
        DataContext = new UpdateArticleViewModel(articleTypeRepository, articleRepository, article, this, service);
    }
}