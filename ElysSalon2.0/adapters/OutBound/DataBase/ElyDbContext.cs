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
                ArticleId = 1,
                Name = "Tinte Rojo",
                ArticleTypeId = 5,
                Description = "MEESI",
                PriceBuy = 12.59M,
                PriceCost = 2.5M,
                Stock = 10
            },
            new Article
            {
                ArticleId = 2,
                Name = "Uñas Acrilicas",
                ArticleTypeId = 3,
                Description = "MEESI",
                PriceBuy = 22.59M,
                PriceCost = 2.5M,
                Stock = 1
            },
            new Article
            {
                ArticleId = 3,
                Name = "Pedicure",
                ArticleTypeId = 3,
                Description = "MEESI",
                PriceBuy = 32.59M,
                PriceCost = 2.5M,
                Stock = 1
            },
            new Article
            {
                ArticleId = 4,
                Name = "Manicure",
                ArticleTypeId = 3,
                Description = "MEESI",
                PriceBuy = 52.59M,
                PriceCost = 1.5M,
                Stock = 1
            },
            new Article
            {
                ArticleId = 5,
                Name = "Corte Hombre",
                ArticleTypeId = 3,
                Description = "MEESI",
                PriceBuy = 5.5M,
                PriceCost = 2.5M,
                Stock = 1
            },
            new Article
            {
                ArticleId = 6,
                Name = "Corte Mujer",
                ArticleTypeId = 3,
                Description = "MEESI",
                PriceBuy = 7.59M,
                PriceCost = 3.5M,
                Stock = 1
            },
            new Article
            {
                ArticleId = 7,
                Name = "Aritos",
                ArticleTypeId = 5,
                Description = "MEESI",
                PriceBuy = 32.59M,
                PriceCost = 2.5M,
                Stock = 15
            },
            new Article
            {
                ArticleId = 8,
                Name = "Pestañas",
                ArticleTypeId = 5,
                Description = "MEESI",
                PriceBuy = 52.59M,
                PriceCost = 1.5M,
                Stock = 10
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
            new Sales { SaleId = 100, SaleDate = new DateTime(2025, 3, 15), Total = 20.50m },
            new Sales { SaleId = 101, SaleDate = new DateTime(2025, 3, 16), Total = 59.30m },
            new Sales { SaleId = 102, SaleDate = new DateTime(2025, 3, 17), Total = 10.50m },
            new Sales { SaleId = 103, SaleDate = new DateTime(2025, 3, 18), Total = 23.70m },
            new Sales { SaleId = 104, SaleDate = new DateTime(2025, 3, 20), Total = 56.40m },
            new Sales { SaleId = 105, SaleDate = new DateTime(2025, 3, 21), Total = 60.00m },
            new Sales { SaleId = 106, SaleDate = new DateTime(2025, 3, 22), Total = 65.10m },
            new Sales { SaleId = 107, SaleDate = new DateTime(2025, 3, 23), Total = 13.30m },
            new Sales { SaleId = 108, SaleDate = new DateTime(2025, 3, 24), Total = 27.00m },
            new Sales { SaleId = 109, SaleDate = new DateTime(2025, 3, 25), Total = 34.90m },
            new Sales { SaleId = 110, SaleDate = new DateTime(2025, 3, 26), Total = 80.10m },
            new Sales { SaleId = 111, SaleDate = new DateTime(2025, 3, 27), Total = 28.20m },
            new Sales { SaleId = 112, SaleDate = new DateTime(2025, 3, 28), Total = 77.40m },
            new Sales { SaleId = 113, SaleDate = new DateTime(2025, 3, 29), Total = 66.90m },
            new Sales { SaleId = 114, SaleDate = new DateTime(2025, 3, 30), Total = 97.50m },
            new Sales { SaleId = 115, SaleDate = new DateTime(2025, 4, 1), Total = 69.90m },
            new Sales { SaleId = 116, SaleDate = new DateTime(2025, 4, 2), Total = 66.45m },
            new Sales { SaleId = 117, SaleDate = new DateTime(2025, 4, 3), Total = 60.10m },
            new Sales { SaleId = 118, SaleDate = new DateTime(2025, 4, 5), Total = 50.40m },
            new Sales { SaleId = 119, SaleDate = new DateTime(2025, 4, 6), Total = 49.40m },
            new Sales { SaleId = 120, SaleDate = new DateTime(2025, 4, 7), Total = 38.50m },
            new Sales { SaleId = 122, SaleDate = new DateTime(2025, 4, 8), Total = 80.10m },
            new Sales { SaleId = 123, SaleDate = new DateTime(2025, 4, 9), Total = 110.40m },
            new Sales { SaleId = 124, SaleDate = new DateTime(2025, 4, 10), Total = 119.40m },
            new Sales { SaleId = 125, SaleDate = new DateTime(2025, 4, 11), Total = 63.50m },
            new Sales { SaleId = 126, SaleDate = new DateTime(2025, 4, 12), Total = 57.42m },
            new Sales { SaleId = 127, SaleDate = new DateTime(2025, 4, 13), Total = 50.40m },
            new Sales { SaleId = 128, SaleDate = new DateTime(2025, 4, 14), Total = 49.40m },
            new Sales { SaleId = 129, SaleDate = new DateTime(2025, 4, 15), Total = 38.50m },
            new Sales { SaleId = 130, SaleDate = new DateTime(2025, 4, 16), Total = 80.10m },
            new Sales { SaleId = 131, SaleDate = new DateTime(2025, 4, 17), Total = 110.40m },
            new Sales { SaleId = 132, SaleDate = new DateTime(2025, 4, 18), Total = 119.40m },
            new Sales { SaleId = 133, SaleDate = new DateTime(2025, 4, 19), Total = 63.50m }
        );


        modelBuilder.Entity<Ticket>().HasData(
            new Ticket
            {
                TicketId = "001100",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 15),
                TotalAmount = 18.50m
            },
            new Ticket
            {
                TicketId = "001101",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 15),
                TotalAmount = 22.30m
            },
            new Ticket
            {
                TicketId = "001102",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 15),
                TotalAmount = 19.70m
            },
            new Ticket
            {
                TicketId = "001103",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 16),
                TotalAmount = 24.10m
            },
            new Ticket
            {
                TicketId = "001104",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 16),
                TotalAmount = 25.20m
            },
            new Ticket
            {
                TicketId = "001105",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 17),
                TotalAmount = 30.50m
            },
            new Ticket
            {
                TicketId = "001106",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 18),
                TotalAmount = 27.80m
            },
            new Ticket
            {
                TicketId = "001107",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 18),
                TotalAmount = 15.90m
            },
            new Ticket
            {
                TicketId = "001108",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 20),
                TotalAmount = 36.40m
            },
            new Ticket
            {
                TicketId = "001109",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 21),
                TotalAmount = 22.10m
            },
            new Ticket
            {
                TicketId = "001110",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 21),
                TotalAmount = 17.90m
            },
            new Ticket
            {
                TicketId = "001111",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 22),
                TotalAmount = 35.10m
            },
            new Ticket
            {
                TicketId = "001112",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 23),
                TotalAmount = 23.30m
            },
            new Ticket
            {
                TicketId = "001113",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 24),
                TotalAmount = 31.25m
            },
            new Ticket
            {
                TicketId = "001114",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 24),
                TotalAmount = 25.75m
            },
            new Ticket
            {
                TicketId = "001115",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 25),
                TotalAmount = 38.90m
            },
            new Ticket
            {
                TicketId = "001116",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 25),
                TotalAmount = 18.60m
            },
            new Ticket
            {
                TicketId = "001117",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 25),
                TotalAmount = 17.40m
            },
            new Ticket
            {
                TicketId = "001118",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 26),
                TotalAmount = 16.10m
            },
            new Ticket
            {
                TicketId = "001119",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 27),
                TotalAmount = 12.30m
            },
            new Ticket
            {
                TicketId = "001120",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 27),
                TotalAmount = 31.90m
            },
            new Ticket
            {
                TicketId = "001121",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 28),
                TotalAmount = 11.60m
            },
            new Ticket
            {
                TicketId = "001122",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 28),
                TotalAmount = 21.80m
            },
            new Ticket
            {
                TicketId = "001123",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 29),
                TotalAmount = 11.90m
            },
            new Ticket
            {
                TicketId = "001124",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 30),
                TotalAmount = 21.50m
            },
            new Ticket
            {
                TicketId = "001125",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 1),
                TotalAmount = 21.80m
            },
            new Ticket
            {
                TicketId = "001126",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 1),
                TotalAmount = 42.10m
            },
            new Ticket
            {
                TicketId = "001127",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 2),
                TotalAmount = 36.45m
            },
            new Ticket
            {
                TicketId = "001128",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 3),
                TotalAmount = 15.10m
            },
            new Ticket
            {
                TicketId = "001129",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 5),
                TotalAmount = 24.80m
            },
            new Ticket
            {
                TicketId = "001130",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 5),
                TotalAmount = 12.60m
            },
            new Ticket
            {
                TicketId = "001131",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 6),
                TotalAmount = 22.40m
            },
            new Ticket
            {
                TicketId = "001132",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 7),
                TotalAmount = 34.50m
            },
            new Ticket
            {
                TicketId = "001133",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 5),
                TotalAmount = 24.80m
            },
            new Ticket
            {
                TicketId = "001134",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 15),
                TotalAmount = 12.60m
            },
            new Ticket
            {
                TicketId = "001135",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 16),
                TotalAmount = 22.40m
            },
            new Ticket
            {
                TicketId = "001136",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 17),
                TotalAmount = 34.50m
            },
            new Ticket
            {
                TicketId = "001137",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 18),
                TotalAmount = 34.50m
            }
        );
    }
}