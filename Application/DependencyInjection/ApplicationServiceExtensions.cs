using Core.Interfaces.Services;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Configurations;
using Application.Services;

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

        services.AddScoped<ArticleAppService>();
        services.AddScoped<SaleDataAppService>();
        services.AddScoped<SaleReportsService>();
        services.AddScoped<ReportsConfiguration>();
        return services;
    }
}