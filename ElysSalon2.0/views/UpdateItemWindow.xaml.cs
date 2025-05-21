using System.Windows;
using Application.Services;
using ElysSalon2._0.ViewModels;

namespace ElysSalon2._0.views;

/// <summary>
///     Lógica de interacción para UpdateItemWindow.xaml
/// </summary>
public partial class UpdateItemWindow : Window
{
    public UpdateItemWindow(
        int article, ArticleAppService service)
    {
        InitializeComponent();
        DataContext = new UpdateArticleViewModel(article, this, service);
    }
}