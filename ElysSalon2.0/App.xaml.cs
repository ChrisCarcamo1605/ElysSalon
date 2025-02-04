using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.views;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.adapters.OutBound;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ElysSalon2._0;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application {
    private readonly IServiceProvider _serviceProvider;

    public App(){
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services){
        // Capa de datos - Correcto como Scoped
        services.AddScoped<IArticleRepository, ArticleRepository>();

        // Servicios de infraestructura - Bien como Singleton
        services.AddSingleton<WindowsManager>();
        services.AddSingleton<DbUtil>();

        // Ventanas - Bien como Transient
        services.AddTransient<ArticlesWindow>();
        services.AddTransient<AdminWindow>();
        services.AddTransient<MainWindow>();
        services.AddTransient<SalesWindow>();
        services.AddTransient<ConfirmWindow>();
        services.AddTransient<MailWindow>();

        // Managers de lógica de negocio
        services.AddScoped<ItemManager>(); // ← Considera cambiar este a Scoped
    }

    // Este es el método que será llamado al inicio
    protected override void OnStartup(StartupEventArgs e){
        base.OnStartup(e);

        // Inyecta dependencias y muestra la ventana principal
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}