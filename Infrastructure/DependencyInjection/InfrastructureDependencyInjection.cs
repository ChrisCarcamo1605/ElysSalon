using Core.Interfaces.Repositories;
using Infrastructure.Persistence.DataBase;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<ITicketRepository, TicketRepository>();
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        services.AddDbContextFactory<ElyDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}