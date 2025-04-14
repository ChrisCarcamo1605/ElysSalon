using System.Windows;
using ElysSalon2._0.adapters.InBound.ViewModels;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.aplication.Interfaces.Services;

namespace ElysSalon2._0.adapters.InBound.views;

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