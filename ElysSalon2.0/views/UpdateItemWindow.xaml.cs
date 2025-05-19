using System.Windows;

namespace ElysSalon2._0.views;

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