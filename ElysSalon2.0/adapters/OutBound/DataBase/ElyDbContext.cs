using ElysSalon2._0.Core.domain.Entities;
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
    public DbSet<Sales> Sales { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

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

        modelBuilder.Entity<Sales>().HasData(
            // Últimos 7 días (5 registros)
            new Sales { SaleId = 1, SaleDate = new DateTime(2025, 01, 20), Total = 50.00m }, //Fecha estatica
            new Sales { SaleId = 2, SaleDate = new DateTime(2025, 01, 19), Total = 75.50m }, //Fecha estatica
            new Sales { SaleId = 3, SaleDate = new DateTime(2025, 01, 18), Total = 120.25m }, //Fecha estatica
            new Sales { SaleId = 4, SaleDate = new DateTime(2025, 01, 17), Total = 30.75m }, //Fecha estatica
            new Sales { SaleId = 5, SaleDate = new DateTime(2025, 01, 16), Total = 90.00m }, //Fecha estatica

            // Último mes (5 registros)
            new Sales { SaleId = 6, SaleDate = new DateTime(2025, 02, 25), Total = 60.00m }, //Fecha estatica
            new Sales { SaleId = 7, SaleDate = new DateTime(2025, 02, 20), Total = 85.50m }, //Fecha estatica
            new Sales { SaleId = 8, SaleDate = new DateTime(2025, 02, 15), Total = 130.25m }, //Fecha estatica
            new Sales { SaleId = 9, SaleDate = new DateTime(2025, 02, 10), Total = 40.75m }, //Fecha estatica
            new Sales { SaleId = 10, SaleDate = new DateTime(2025, 02, 5), Total = 100.00m }, //Fecha estatica

            // Últimos 3 meses (5 registros)
            new Sales { SaleId = 11, SaleDate = new DateTime(2025, 03, 07), Total = 70.00m }, //Fecha estatica
            new Sales { SaleId = 12, SaleDate = new DateTime(2025, 03, 12), Total = 95.50m }, //Fecha estatica
            new Sales { SaleId = 13, SaleDate = new DateTime(2025, 03, 08), Total = 140.25m }, //Fecha estatica
            new Sales { SaleId = 14, SaleDate = new DateTime(2025, 03, 03), Total = 50.75m }, //Fecha estatica
            new Sales { SaleId = 15, SaleDate = new DateTime(2025, 03, 08), Total = 110.00m } //Fecha estatica
        );

        modelBuilder.Entity<Ticket>().HasData(

            new Ticket { TicketId = "000100", Issuer = "", EmissionDateTime = new DateTime(2025, 01, 02), TotalAmount = 50.00m }, //Fecha estatica
            new Ticket { TicketId = "000101", Issuer = "", EmissionDateTime = new DateTime(2025, 01, 6), TotalAmount = 50.00m }, //Fecha estatica
            new Ticket { TicketId = "000102", Issuer = "", EmissionDateTime = new DateTime(2025, 01, 8), TotalAmount = 100.00m }, //Fecha estatica
            new Ticket { TicketId = "000103", Issuer = "", EmissionDateTime = new DateTime(2025, 01, 13), TotalAmount = 100.00m }, //Fecha estatica
            new Ticket { TicketId = "000104", Issuer = "", EmissionDateTime = new DateTime(2025, 01, 18), TotalAmount = 200.00m }, //Fecha estatica

            new Ticket { TicketId = "000105", Issuer = "", EmissionDateTime = new DateTime(2025, 01, 23), TotalAmount = 200.00m }, //Fecha estatica
            new Ticket { TicketId = "000106", Issuer = "", EmissionDateTime = new DateTime(2025, 01, 25), TotalAmount = 400.00m }, //Fecha estatica
            new Ticket { TicketId = "000107", Issuer = "", EmissionDateTime = new DateTime(2025, 01, 29), TotalAmount = 400.00m }, //Fecha estatica
            new Ticket { TicketId = "000108", Issuer = "", EmissionDateTime = new DateTime(2025, 02, 02), TotalAmount = 800.00m }, //Fecha estatica
            new Ticket { TicketId = "000109", Issuer = "", EmissionDateTime = new DateTime(2025, 02, 05), TotalAmount = 800.00m },
            new Ticket { TicketId = "000110", Issuer = "", EmissionDateTime = new DateTime(2025, 02, 011), TotalAmount = 1600.00m },
            new Ticket { TicketId = "000111", Issuer = "", EmissionDateTime = new DateTime(2025, 02, 014), TotalAmount = 1600.00m }, //Fecha estatica

            new Ticket { TicketId = "000112", Issuer = "", EmissionDateTime = new DateTime(2025, 02, 19), TotalAmount = 3200.00m }, //Fecha estatica
            new Ticket { TicketId = "000113", Issuer = "", EmissionDateTime = new DateTime(2025, 02, 21), TotalAmount = 3200.00m }, //Fecha estatica
            new Ticket { TicketId = "000114", Issuer = "", EmissionDateTime = new DateTime(2025, 02, 24), TotalAmount = 6400.00m }, //Fecha estatica
            new Ticket { TicketId = "000115", Issuer = "", EmissionDateTime = new DateTime(2025, 02, 27), TotalAmount = 6400.00m }, //Fecha estatica
            new Ticket { TicketId = "000116", Issuer = "", EmissionDateTime = new DateTime(2025, 02, 26), TotalAmount = 128000.00m },
            new Ticket { TicketId = "000117", Issuer = "", EmissionDateTime = new DateTime(2025, 03, 06), TotalAmount = 128000.00m },
            new Ticket { TicketId = "000118", Issuer = "", EmissionDateTime = new DateTime(2025, 03, 02), TotalAmount = 256000.00m },
            new Ticket { TicketId = "000119", Issuer = "", EmissionDateTime = new DateTime(2025, 03, 09), TotalAmount = 256000.00m },
            new Ticket { TicketId = "000120", Issuer = "", EmissionDateTime = new DateTime(2025, 03, 12), TotalAmount = 512000.00m },
            new Ticket { TicketId = "000121", Issuer = "", EmissionDateTime = new DateTime(2025, 03, 16), TotalAmount = 512000.00m },
            new Ticket { TicketId = "000122", Issuer = "", EmissionDateTime = new DateTime(2025, 03, 19), TotalAmount = 600000.00m },
            new Ticket { TicketId = "000123", Issuer = "", EmissionDateTime = new DateTime(2025, 03, 22), TotalAmount = 600000.00m },
            new Ticket { TicketId = "000124", Issuer = "", EmissionDateTime = new DateTime(2025, 03, 26), TotalAmount = 800000.00m }

        //Fecha estatica
        );
    }
}