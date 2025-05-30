using System.Windows;
using Application.DependencyInjection;
using ElysSalon2._0.DependencyInjection;
using ElysSalon2._0.views;
using ElysSalon2._0.WinManagement;
using Infrastructure.DependencyInjection;
using Infrastructure.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0;

/// <summary>
///     Interaction logic for App.xaml
/// </summary>
public partial class App : System.Windows.Application
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
        services.AddPresentation();
        services.AddAplication();
        services.AddInfrastructure(
            "Server=localhost,1433;Database=elysalondb;User Id=sa;Password=Carcamito*-*2024$1605;TrustServerCertificate=True;");

        services.AddSingleton<WindowsManager>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ElyDbContext>>();
            using var dbContext = dbContextFactory.CreateDbContext();
            dbContext.Database.Migrate();
        }

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}