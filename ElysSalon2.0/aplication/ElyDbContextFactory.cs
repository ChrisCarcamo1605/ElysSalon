using ElysSalon2._0.aplication.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElysSalon2._0.aplication;

public class ElyDbContextFactory:IDesignTimeDbContextFactory<ElyDbContext>
{
    public ElyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ElyDbContext>();
        optionsBuilder.UseSqlServer(SecretManager.GetValue("userCon"));

        return new ElyDbContext(optionsBuilder.Options);
    }
}