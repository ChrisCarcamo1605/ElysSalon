using ElysSalon2._0.aplication.Management;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElysSalon2._0.adapters.OutBound.DataBase;

public class ElyDbContextFactory:IDesignTimeDbContextFactory<ElyDbContext>
{
    public ElyDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ElyDbContext>();

        //Preferably is better use secrets to most security

        // optionsBuilder.UseSqlServer(SecretManager.GetValue("userCon"));
        

        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=elysalondb;User Id=sa;Password=Carcamito*-*2024$1605;TrustServerCertificate=True;");
        return new ElyDbContext(optionsBuilder.Options);
    }
}