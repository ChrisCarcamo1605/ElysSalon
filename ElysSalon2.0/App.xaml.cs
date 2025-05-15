using System.Windows;
using ElysSalon2._0.adapters.InBound.ViewModels;
using ElysSalon2._0.adapters.InBound.views;
using ElysSalon2._0.adapters.OutBound.DataBase;
using ElysSalon2._0.adapters.OutBound.Repositories;
using ElysSalon2._0.aplication.Interfaces.Repositories;
using ElysSalon2._0.aplication.Interfaces.Services;
using ElysSalon2._0.aplication.Management;
using ElysSalon2._0.domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0;

/// <summary>
///     Interaction logic for App.xaml
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
        //ViewModels
        services.AddTransient<TypesManagementViewModel>();
        services.AddTransient<UpdateArticleViewModel>();
        services.AddTransient<ItemManagerViewModel>();
        services.AddTransient<ShoppingCartViewModel>();
        services.AddScoped<SalesViewModel>();
        services.AddScoped<ChartsViewModel>();


        //Interfaces and Repositories
        services.AddTransient<IArticleRepository, ArticleRepository>();
        services.AddTransient<IArticleTypeRepository, ArticleTypeRepository>();
        services.AddSingleton<WindowsManager>();
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

        //Services
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ISalesReportsService, SaleReportsService>();
        services.AddScoped<ISalesDataService, SalesDataService>();


        //Configuration
        services.AddTransient<ReportsConfiguration>();

        //Windows
        services.AddTransient<AdminWindow>();
        services.AddTransient<MainWindow>();
        services.AddTransient<SalesWindow>();
        services.AddTransient<ConfirmWindow>();
        services.AddTransient<MailWindow>();
        services.AddTransient<UpdateItemWindow>();
        services.AddTransient<ItemManagerWindow>();
        services.AddTransient<TypeArticleWindow>();
        services.AddTransient<ShoppingCartWindow>();
        services.AddScoped<ChartsWindow>();

        //Mapper
        services.AddAutoMapper(typeof(App).Assembly);

        //DbContext
        services.AddDbContextFactory<ElyDbContext>(options => options.UseSqlServer(
            "Server=localhost,1433;Database=elysalondb;User Id=sa;Password=Carcamito*-*2024$1605;TrustServerCertificate=True;"));
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using (var scope = _serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ElyDbContext>();
            context.Database.Migrate();
        }

        var mainWindow = _serviceProvider.GetRequiredService<SalesWindow>();
        mainWindow.Show();
    }
}