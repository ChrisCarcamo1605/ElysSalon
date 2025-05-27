using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DataBase;

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
    public DbSet<Expense> Expense { get; set; }


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
                TicketId = "T-000100",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 15),
                TotalAmount = 18.50m
            },
            new Ticket
            {
                TicketId = "T-000101",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 15),
                TotalAmount = 22.30m
            },
            new Ticket
            {
                TicketId = "T-000102",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 15),
                TotalAmount = 19.70m
            },
            new Ticket
            {
                TicketId = "T-000103",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 16),
                TotalAmount = 24.10m
            },
            new Ticket
            {
                TicketId = "T-000104",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 16),
                TotalAmount = 25.20m
            },
            new Ticket
            {
                TicketId = "T-000105",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 17),
                TotalAmount = 30.50m
            },
            new Ticket
            {
                TicketId = "T-000106",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 18),
                TotalAmount = 27.80m
            },
            new Ticket
            {
                TicketId = "T-000107",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 18),
                TotalAmount = 15.90m
            },
            new Ticket
            {
                TicketId = "T-000108",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 20),
                TotalAmount = 36.40m
            },
            new Ticket
            {
                TicketId = "T-000109",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 21),
                TotalAmount = 22.10m
            },
            new Ticket
            {
                TicketId = "T-000110",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 21),
                TotalAmount = 17.90m
            },
            new Ticket
            {
                TicketId = "T-000111",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 22),
                TotalAmount = 35.10m
            },
            new Ticket
            {
                TicketId = "T-000112",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 23),
                TotalAmount = 23.30m
            },
            new Ticket
            {
                TicketId = "T-000113",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 24),
                TotalAmount = 31.25m
            },
            new Ticket
            {
                TicketId = "T-000114",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 24),
                TotalAmount = 25.75m
            },
            new Ticket
            {
                TicketId = "T-000115",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 25),
                TotalAmount = 38.90m
            },
            new Ticket
            {
                TicketId = "T-000116",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 25),
                TotalAmount = 18.60m
            },
            new Ticket
            {
                TicketId = "T-000117",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 25),
                TotalAmount = 17.40m
            },
            new Ticket
            {
                TicketId = "T-000118",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 26),
                TotalAmount = 16.10m
            },
            new Ticket
            {
                TicketId = "T-000119",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 27),
                TotalAmount = 12.30m
            },
            new Ticket
            {
                TicketId = "T-000120",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 27),
                TotalAmount = 31.90m
            },
            new Ticket
            {
                TicketId = "T-000121",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 28),
                TotalAmount = 11.60m
            },
            new Ticket
            {
                TicketId = "T-000122",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 28),
                TotalAmount = 21.80m
            },
            new Ticket
            {
                TicketId = "T-000123",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 29),
                TotalAmount = 11.90m
            },
            new Ticket
            {
                TicketId = "T-000124",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 3, 30),
                TotalAmount = 21.50m
            },
            new Ticket
            {
                TicketId = "T-000125",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 1),
                TotalAmount = 21.80m
            },
            new Ticket
            {
                TicketId = "T-000126",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 1),
                TotalAmount = 42.10m
            },
            new Ticket
            {
                TicketId = "T-000127",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 2),
                TotalAmount = 36.45m
            },
            new Ticket
            {
                TicketId = "T-000128",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 3),
                TotalAmount = 15.10m
            },
            new Ticket
            {
                TicketId = "T-000129",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 5),
                TotalAmount = 24.80m
            },
            new Ticket
            {
                TicketId = "T-000130",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 5),
                TotalAmount = 12.60m
            },
            new Ticket
            {
                TicketId = "T-000131",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 6),
                TotalAmount = 22.40m
            },
            new Ticket
            {
                TicketId = "T-000132",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 7),
                TotalAmount = 34.50m
            },
            new Ticket
            {
                TicketId = "T-000133",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 5),
                TotalAmount = 24.80m
            },
            new Ticket
            {
                TicketId = "T-000134",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 15),
                TotalAmount = 12.60m
            },
            new Ticket
            {
                TicketId = "T-000135",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 16),
                TotalAmount = 22.40m
            },
            new Ticket
            {
                TicketId = "T-000136",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 17),
                TotalAmount = 34.50m
            },
            new Ticket
            {
                TicketId = "T-000137",
                Issuer = "",
                EmissionDateTime = new DateTime(2025, 4, 18),
                TotalAmount = 34.50m
            }
        );

        modelBuilder.Entity<TicketDetails>().HasData(
     new TicketDetails
     {
         TicketDetailsId = 422,
         TicketId = "T-000100",
         ArticleId = 5,
         ArticleName = "Tinte Rojo",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 5, 15) // Fecha específica, no se cambia
     },
     new TicketDetails
     {
         TicketDetailsId = 423,
         TicketId = "T-000100",
         ArticleId = 5,
         ArticleName = "Tinte Rojo",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 4, 27) // 1 mes antes
     },
     new TicketDetails
     {
         TicketDetailsId = 424,
         TicketId = "T-000100",
         ArticleId = 5,
         ArticleName = "Desde un Mes",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 5, 3) // Dentro del rango
     },
     new TicketDetails
     {
         TicketDetailsId = 425,
         TicketId = "T-000100",
         ArticleId = 5,
         ArticleName = "Desde hace 3 meses",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 5, 10) // Dentro del rango
     },
     new TicketDetails
     {
         TicketDetailsId = 426,
         TicketId = "T-000100",
         ArticleId = 6,
         ArticleName = "Desde un Mes",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 4, 29) // Dentro del rango
     },
     new TicketDetails
     {
         TicketDetailsId = 427,
         TicketId = "T-000100",
         ArticleId = 9,
         ArticleName = "Desde hace 3 meses",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 5, 1) // Dentro del rango
     },
     new TicketDetails
     {
         TicketDetailsId = 428,
         TicketId = "T-000100",
         ArticleId = 4,
         ArticleName = "Desde hace 3 meses",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 5, 18) // Dentro del rango
     },
     new TicketDetails
     {
         TicketDetailsId = 429,
         TicketId = "T-000100",
         ArticleId = 9,
         ArticleName = "Desde hace 3 meses",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 5, 25) // Cerca de la fecha actual
     },
     new TicketDetails
     {
         TicketDetailsId = 430,
         TicketId = "T-000100",
         ArticleId = 7,
         ArticleName = "Desde hace 3 meses",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 5, 20) // Dentro del rango
     },
     new TicketDetails
     {
         TicketDetailsId = 431,
         TicketId = "T-000100",
         ArticleId = 7,
         ArticleName = "Desde hace 1 mes",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 5, 12) // Dentro del rango
     },
     new TicketDetails
     {
         TicketDetailsId = 432,
         TicketId = "T-000100",
         ArticleId = 7,
         ArticleName = "Desde hace 3 meses",
         Quantity = 1,
         Price = 12.59M,
         Date = new DateTime(2025, 5, 6) // Dentro del rango
     }
 );
        modelBuilder.Entity<Expense>().HasData(
            new Expense { Id = 1, Amount = 25.50m, Reason = "Compra de suministros", Date = new DateTime(2025, 4, 30) },
            new Expense { Id = 2, Amount = 15.75m, Reason = "Almuerzo", Date = new DateTime(2025, 5, 1) },
            new Expense { Id = 3, Amount = 80.00m, Reason = "Combustible", Date = new DateTime(2025, 5, 2) },
            new Expense { Id = 4, Amount = 35.20m, Reason = "Reparación menor", Date = new DateTime(2025, 5, 3) },
            new Expense { Id = 5, Amount = 12.99m, Reason = "Suscripción online", Date = new DateTime(2025, 5, 4) },
            new Expense { Id = 6, Amount = 210.50m, Reason = "Viaje de negocios", Date = new DateTime(2025, 5, 5) },
            new Expense { Id = 7, Amount = 45.00m, Reason = "Material de oficina", Date = new DateTime(2025, 4, 25) },
            new Expense { Id = 8, Amount = 65.80m, Reason = "Pago de internet", Date = new DateTime(2025, 4, 20) },
            new Expense { Id = 9, Amount = 200.00m, Reason = "Alquiler de local", Date = new DateTime(2025, 3, 10) },
            new Expense { Id = 10, Amount = 90.30m, Reason = "Curso online", Date = new DateTime(2025, 3, 25) },
            new Expense { Id = 11, Amount = 18.75m, Reason = "Mantenimiento web", Date = new DateTime(2025, 4, 15) },
            new Expense { Id = 12, Amount = 75.00m, Reason = "Publicidad Facebook", Date = new DateTime(2025, 3, 5) },
            new Expense { Id = 13, Amount = 5.50m, Reason = "Impresiones", Date = new DateTime(2025, 4, 1) },
            new Expense
            {
                Id = 14, Amount = 30.25m, Reason = "Productos de limpieza", Date = new DateTime(2025, 3, 18)
            },
            new Expense { Id = 15, Amount = 40.00m, Reason = "Soporte técnico", Date = new DateTime(2025, 4, 8) },
            new Expense
            {
                Id = 16, Amount = 150.00m, Reason = "Licencia de software", Date = new DateTime(2025, 3, 22)
            },
            new Expense { Id = 17, Amount = 55.60m, Reason = "Seguro", Date = new DateTime(2025, 4, 12) },
            new Expense { Id = 18, Amount = 12.30m, Reason = "Envío de documentos", Date = new DateTime(2025, 3, 28) },
            new Expense { Id = 19, Amount = 300.00m, Reason = "Consultoría", Date = new DateTime(2025, 4, 5) },
            new Expense { Id = 20, Amount = 85.40m, Reason = "Entrada a evento", Date = new DateTime(2025, 3, 15) },
            new Expense { Id = 21, Amount = 28.90m, Reason = "Suministros varios", Date = new DateTime(2025, 4, 28) },
            new Expense { Id = 22, Amount = 18.50m, Reason = "Cafetería", Date = new DateTime(2025, 5, 2) },
            new Expense
            {
                Id = 23, Amount = 95.00m, Reason = "Mantenimiento vehículo", Date = new DateTime(2025, 4, 22)
            },
            new Expense { Id = 24, Amount = 42.15m, Reason = "Arreglo de oficina", Date = new DateTime(2025, 3, 30) },
            new Expense { Id = 25, Amount = 7.99m, Reason = "App de productividad", Date = new DateTime(2025, 5, 1) },
            new Expense { Id = 26, Amount = 180.75m, Reason = "Alojamiento", Date = new DateTime(2025, 4, 18) },
            new Expense { Id = 27, Amount = 38.20m, Reason = "Papelería", Date = new DateTime(2025, 3, 8) },
            new Expense { Id = 28, Amount = 70.50m, Reason = "Pago de agua", Date = new DateTime(2025, 4, 26) },
            new Expense { Id = 29, Amount = 100.00m, Reason = "Publicidad impresa", Date = new DateTime(2025, 3, 12) },
            new Expense { Id = 30, Amount = 105.60m, Reason = "Seminario", Date = new DateTime(2025, 4, 3) },
            new Expense { Id = 31, Amount = 22.40m, Reason = "Actualización web", Date = new DateTime(2025, 3, 20) },
            new Expense { Id = 32, Amount = 88.00m, Reason = "Google Ads", Date = new DateTime(2025, 4, 10) },
            new Expense { Id = 33, Amount = 9.20m, Reason = "Fotocopias", Date = new DateTime(2025, 3, 27) },
            new Expense { Id = 34, Amount = 33.90m, Reason = "Material de aseo", Date = new DateTime(2025, 4, 16) },
            new Expense { Id = 35, Amount = 58.15m, Reason = "Repuestos", Date = new DateTime(2025, 3, 5) },
            new Expense { Id = 36, Amount = 165.00m, Reason = "Software contable", Date = new DateTime(2025, 4, 2) });
    }
}