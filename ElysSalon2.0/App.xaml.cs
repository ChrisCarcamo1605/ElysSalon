using System.Configuration;
using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.views;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.adapters.OutBound;
using ElysSalon2._0.aplication;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.aplication.Repositories;
using ElysSalon2._0.aplication.Utils;
using ElysSalon2._0.aplication.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ElysSalon2._0;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;

    public App()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<UpdateArticleViewModel>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IArticleTypeRepository, ArticleTypeRepository>();
        services.AddSingleton<WindowsManager>();
        services.AddSingleton<DbUtil>();
        services.AddTransient<ItemManagerViewModel>();
        services.AddTransient<ShoppingCartWindow>();
        services.AddTransient<AdminWindow>();
        services.AddTransient<MainWindow>();
        services.AddTransient<SalesWindow>();
        services.AddTransient<ConfirmWindow>();
        services.AddTransient<MailWindow>();
        services.AddAutoMapper(typeof(App).Assembly);
        services.AddTransient<TypeArticleWindow>();
        services.AddTransient<ItemManager>();
        services.AddDbContext<ElyDbContext>( options => options.UseSqlServer(
                "Server=CHRIS\\CHRISSERVER;Database=elysalondb;User ID=sa;Password=1234;TrustServerCertificate=True;"));

    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ElyDbContext>();
            context.Database.Migrate();
        }

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}