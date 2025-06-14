using System.IO;
using System.Windows;
using Application.DependencyInjection;
using ElysSalon2._0.DependencyInjection;
using ElysSalon2._0.views;
using ElysSalon2._0.WinManagement;
using Infrastructure.DependencyInjection;
using Infrastructure.Persistence.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        // Configuración desde archivo
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        // Configuración de capas
        services.AddPresentation();
        services.AddAplication();

        // Cadena de conexión desde configuración
        services.AddInfrastructure(configuration,
            "Server=localhost,1433;Database=elysalondb;User Id=sa;Password=Carcamito*-*2024$1605;TrustServerCertificate=True;");

        // Otros servicios
        services.AddSingleton<WindowsManager>();
        services.AddTransient<SalesWindow>();
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

        var mainWindow = _serviceProvider.GetRequiredService<SalesWindow>();
        mainWindow.Show();
    }
}