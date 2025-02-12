using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.views;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.adapters.OutBound;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
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
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IArticleTypeRepository, ArticleTypeRepository>();
        services.AddSingleton<WindowsManager>();
        services.AddSingleton<DbUtil>();
        services.AddTransient<ArticlesWindow>();
        services.AddTransient<AdminWindow>();
        services.AddTransient<MainWindow>();
        services.AddTransient<SalesWindow>();
        services.AddTransient<ConfirmWindow>();
        services.AddTransient<MailWindow>();
        services.AddTransient<TypeArticleWindow>();
        services.AddTransient<ItemManager>();
    }

    protected override void OnStartup(StartupEventArgs e){
        base.OnStartup(e);
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}