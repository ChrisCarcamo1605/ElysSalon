using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Services;
using ElysSalon2._0.domain.Entities;
using System.Windows;
using ElysSalon2._0.aplication.ViewModels;

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