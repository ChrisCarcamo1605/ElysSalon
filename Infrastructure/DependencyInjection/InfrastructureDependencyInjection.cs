using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Persistence.DataBase;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Service;
using MailKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MailService = Infrastructure.Service.MailService;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string connectionString)
    {
        services.AddTransient<ITicketRepository, TicketRepository>();
        services.AddTransient<IEmailService,MailService>();
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IEmailService,MailService>();
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddDbContextFactory<ElyDbContext>(options =>
            options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

        return services;
    }
}