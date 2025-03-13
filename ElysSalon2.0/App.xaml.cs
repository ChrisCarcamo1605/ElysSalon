﻿using System.Windows;
using ElysSalon2._0.adapters.InBound.UI.ViewModels;
using ElysSalon2._0.adapters.InBound.UI.views;
using ElysSalon2._0.adapters.InBound.UI.views.AdminViews;
using ElysSalon2._0.adapters.InBound.UI.views.MainViews;
using ElysSalon2._0.adapters.OutBound.DataBase;
using ElysSalon2._0.adapters.OutBound.Repositories;
using ElysSalon2._0.Core.aplication.Management;
using ElysSalon2._0.Core.aplication.Ports.Repositories;
using ElysSalon2._0.Core.aplication.Ports.Services;
using ElysSalon2._0.Core.domain.Services;
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
        services.AddTransient<SalesViewModel>();

        //Interfaces/Services and Repositories
        services.AddTransient<IArticleRepository, ArticleRepository>();
        services.AddTransient<IArticleTypeRepository, ArticleTypeRepository>();
        services.AddTransient<IArticleService, ArticleService>();
        services.AddSingleton<WindowsManager>();
        services.AddTransient<ITicketRepository, TicketRepository>();
        services.AddTransient<ITicketService, TicketService>();
        services.AddTransient<ISalesRepository, SalesRepository>();

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

        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}