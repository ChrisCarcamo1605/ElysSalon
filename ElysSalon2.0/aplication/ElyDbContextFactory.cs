using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElysSalon2._0.aplication;

public class ElyDbContextFactory:IDesignTimeDbContextFactory<ElyDbContext>
{
    public ElyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ElyDbContext>();
        optionsBuilder.UseSqlServer("Server=CHRIS\\CHRISSERVER;Database=elysalondb;User ID=sa;Password=1234;TrustServerCertificate=True;");

        return new ElyDbContext(optionsBuilder.Options);
    }
}