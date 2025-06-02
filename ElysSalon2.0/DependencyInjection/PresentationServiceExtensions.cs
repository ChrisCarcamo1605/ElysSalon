using ElysSalon2._0.Services;
using ElysSalon2._0.ViewModels;
using ElysSalon2._0.views;
using Microsoft.Extensions.DependencyInjection;

namespace ElysSalon2._0.DependencyInjection;

public static class PresentationServiceExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        //ViewModels
        services.AddTransient<TypesManagementViewModel>();
        services.AddTransient<UpdateArticleViewModel>();
        services.AddTransient<ItemManagerViewModel>();
        services.AddTransient<ShoppingCartViewModel>();
        services.AddScoped<SalesViewModel>();
        services.AddScoped<SaleReportsService>();

        services.AddScoped<ChartsViewModel>();
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

        services.AddAutoMapper(typeof(PresentationServiceExtensions).Assembly);
        
        return services;
    }
}