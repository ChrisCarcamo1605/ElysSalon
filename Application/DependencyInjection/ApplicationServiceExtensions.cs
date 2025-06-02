using Application.Configurations;
using Application.Services;
using AutoMapper;
using Core.Interfaces.Services;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddAplication(this IServiceCollection services)
    {
        //Services
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IArtTypeService, ArtTypeService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<ITicketDetailsService, TicketDetailsService>();
        services.AddScoped<ISalesService, SalesService>();
        services.AddScoped<IExpensesService, ExpenseService>();
        services.AddScoped<IReportsService, ReportsAppService>();

        services.AddScoped<ArticleAppService>();
        services.AddScoped<SaleDataAppService>();
        services.AddScoped<ReportsConfiguration>();

        services.AddAutoMapper(typeof(ApplicationServiceExtensions).Assembly);
        return services;
    }
}