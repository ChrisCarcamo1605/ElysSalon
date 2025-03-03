using ElysSalon2._0.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElysSalon2._0.adapters.OutBound.DataBase;

public class ElyDbContext : DbContext
{
    public ElyDbContext(DbContextOptions<ElyDbContext> options) : base(options)
    {
    }

    public DbSet<Article> Article { get; set; }
    public DbSet<ArticleType> ArticleType { get; set; }
    public DbSet<TicketDetails> TicketDetails { get; set; }
    public DbSet<Ticket> Tickets { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArticleType>().HasData(
            new ArticleType { ArticleTypeId = 1, Name = "Todo" },
            new ArticleType { ArticleTypeId = 2, Name = "Elegir Tipo" },
            new ArticleType { ArticleTypeId = 3, Name = "Cabello" },
            new ArticleType { ArticleTypeId = 4, Name = "Servicio" },
            new ArticleType { ArticleTypeId = 5, Name = "Tintes" },
            new ArticleType { ArticleTypeId = 6, Name = "Producto" });



        modelBuilder.Entity<Article>().HasData(
            new Article
            {
                ArticleId = 1, Name = "Tinte Rojo", ArticleTypeId = 5, Description = "MEESI", PriceBuy = 12.59M,
                PriceCost = 2.5M, Stock = 10
            },
            new Article
            {
                ArticleId = 2, Name = "Uñas Acrilicas", ArticleTypeId = 3, Description = "MEESI", PriceBuy = 22.59M,
                PriceCost = 2.5M, Stock = 1
            },
            new Article
            {
                ArticleId = 3, Name = "Pedicure", ArticleTypeId = 3, Description = "MEESI", PriceBuy = 32.59M,
                PriceCost = 2.5M, Stock = 1
            },
            new Article
            {
                ArticleId = 4, Name = "Manicure", ArticleTypeId = 3, Description = "MEESI", PriceBuy = 52.59M,
                PriceCost = 1.5M, Stock = 1
            },
            new Article
            {
                ArticleId = 5, Name = "Corte Hombre", ArticleTypeId = 3, Description = "MEESI", PriceBuy = 5.5M,
                PriceCost = 2.5M, Stock = 1
            },
            new Article
            {
                ArticleId = 6, Name = "Corte Mujer", ArticleTypeId = 3, Description = "MEESI", PriceBuy = 7.59M,
                PriceCost = 3.5M, Stock = 1
            },
            new Article
            {
                ArticleId = 7, Name = "Aritos", ArticleTypeId = 5, Description = "MEESI", PriceBuy = 32.59M,
                PriceCost = 2.5M, Stock = 15
            },
            new Article
            {
                ArticleId = 8, Name = "Pestañas", ArticleTypeId = 5, Description = "MEESI", PriceBuy = 52.59M,
                PriceCost = 1.5M, Stock = 10
            },
            new Article
            {
                ArticleId = 9,
                Name = "Depilado Cejas",
                ArticleTypeId = 3,
                Description = "MEESI",
                PriceBuy = 42.59M,
                PriceCost = 1.6M,
                Stock = 1
            },
            new Article
            {
                ArticleId = 10,
                Name = "Kersel",
                ArticleTypeId = 3,
                Description = "MEESI",
                PriceBuy = 65.59M,
                PriceCost = 11.6M,
                Stock = 21
            }
        );
    }
}