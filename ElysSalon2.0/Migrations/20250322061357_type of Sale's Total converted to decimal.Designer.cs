﻿// <auto-generated />
using System;
using ElysSalon2._0.adapters.OutBound.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ElysSalon2._0.Migrations
{
    [DbContext(typeof(ElyDbContext))]
    [Migration("20250322061357_type of Sale's Total converted to decimal")]
    partial class typeofSalesTotalconvertedtodecimal
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ElysSalon2._0.Core.domain.Entities.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleId"));

                    b.Property<int>("ArticleTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PriceBuy")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PriceCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ArticleId");

                    b.HasIndex("ArticleTypeId");

                    b.ToTable("Article");

                    b.HasData(
                        new
                        {
                            ArticleId = 1,
                            ArticleTypeId = 5,
                            Description = "MEESI",
                            Name = "Tinte Rojo",
                            PriceBuy = 12.59m,
                            PriceCost = 2.5m,
                            Stock = 10
                        },
                        new
                        {
                            ArticleId = 2,
                            ArticleTypeId = 3,
                            Description = "MEESI",
                            Name = "Uñas Acrilicas",
                            PriceBuy = 22.59m,
                            PriceCost = 2.5m,
                            Stock = 1
                        },
                        new
                        {
                            ArticleId = 3,
                            ArticleTypeId = 3,
                            Description = "MEESI",
                            Name = "Pedicure",
                            PriceBuy = 32.59m,
                            PriceCost = 2.5m,
                            Stock = 1
                        },
                        new
                        {
                            ArticleId = 4,
                            ArticleTypeId = 3,
                            Description = "MEESI",
                            Name = "Manicure",
                            PriceBuy = 52.59m,
                            PriceCost = 1.5m,
                            Stock = 1
                        },
                        new
                        {
                            ArticleId = 5,
                            ArticleTypeId = 3,
                            Description = "MEESI",
                            Name = "Corte Hombre",
                            PriceBuy = 5.5m,
                            PriceCost = 2.5m,
                            Stock = 1
                        },
                        new
                        {
                            ArticleId = 6,
                            ArticleTypeId = 3,
                            Description = "MEESI",
                            Name = "Corte Mujer",
                            PriceBuy = 7.59m,
                            PriceCost = 3.5m,
                            Stock = 1
                        },
                        new
                        {
                            ArticleId = 7,
                            ArticleTypeId = 5,
                            Description = "MEESI",
                            Name = "Aritos",
                            PriceBuy = 32.59m,
                            PriceCost = 2.5m,
                            Stock = 15
                        },
                        new
                        {
                            ArticleId = 8,
                            ArticleTypeId = 5,
                            Description = "MEESI",
                            Name = "Pestañas",
                            PriceBuy = 52.59m,
                            PriceCost = 1.5m,
                            Stock = 10
                        },
                        new
                        {
                            ArticleId = 9,
                            ArticleTypeId = 3,
                            Description = "MEESI",
                            Name = "Depilado Cejas",
                            PriceBuy = 42.59m,
                            PriceCost = 1.6m,
                            Stock = 1
                        },
                        new
                        {
                            ArticleId = 10,
                            ArticleTypeId = 3,
                            Description = "MEESI",
                            Name = "Kersel",
                            PriceBuy = 65.59m,
                            PriceCost = 11.6m,
                            Stock = 21
                        });
                });

            modelBuilder.Entity("ElysSalon2._0.Core.domain.Entities.ArticleType", b =>
                {
                    b.Property<int>("ArticleTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArticleTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArticleTypeId");

                    b.ToTable("ArticleType");

                    b.HasData(
                        new
                        {
                            ArticleTypeId = 1,
                            Name = "Todo"
                        },
                        new
                        {
                            ArticleTypeId = 2,
                            Name = "Elegir Tipo"
                        },
                        new
                        {
                            ArticleTypeId = 3,
                            Name = "Cabello"
                        },
                        new
                        {
                            ArticleTypeId = 4,
                            Name = "Servicio"
                        },
                        new
                        {
                            ArticleTypeId = 5,
                            Name = "Tintes"
                        },
                        new
                        {
                            ArticleTypeId = 6,
                            Name = "Producto"
                        });
                });

            modelBuilder.Entity("ElysSalon2._0.Core.domain.Entities.Sales", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleId"));

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SaleId");

                    b.ToTable("Sales");

                    b.HasData(
                        new
                        {
                            SaleId = 1,
                            SaleDate = new DateTime(2025, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 50.00m
                        },
                        new
                        {
                            SaleId = 2,
                            SaleDate = new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 75.50m
                        },
                        new
                        {
                            SaleId = 3,
                            SaleDate = new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 120.25m
                        },
                        new
                        {
                            SaleId = 4,
                            SaleDate = new DateTime(2025, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 30.75m
                        },
                        new
                        {
                            SaleId = 5,
                            SaleDate = new DateTime(2025, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 90.00m
                        },
                        new
                        {
                            SaleId = 6,
                            SaleDate = new DateTime(2025, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 60.00m
                        },
                        new
                        {
                            SaleId = 7,
                            SaleDate = new DateTime(2025, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 85.50m
                        },
                        new
                        {
                            SaleId = 8,
                            SaleDate = new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 130.25m
                        },
                        new
                        {
                            SaleId = 9,
                            SaleDate = new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 40.75m
                        },
                        new
                        {
                            SaleId = 10,
                            SaleDate = new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 100.00m
                        },
                        new
                        {
                            SaleId = 11,
                            SaleDate = new DateTime(2025, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 70.00m
                        },
                        new
                        {
                            SaleId = 12,
                            SaleDate = new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 95.50m
                        },
                        new
                        {
                            SaleId = 13,
                            SaleDate = new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 140.25m
                        },
                        new
                        {
                            SaleId = 14,
                            SaleDate = new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 50.75m
                        },
                        new
                        {
                            SaleId = 15,
                            SaleDate = new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 110.00m
                        });
                });

            modelBuilder.Entity("ElysSalon2._0.Core.domain.Entities.Ticket", b =>
                {
                    b.Property<string>("TicketId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EmissionDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Issuer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalOutTaxes")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalWithTaxes")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TicketId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ElysSalon2._0.Core.domain.Entities.TicketDetails", b =>
                {
                    b.Property<int>("TicketDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketDetailsId"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

                    b.Property<string>("ArticleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("TicketId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TicketDetailsId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("TicketId");

                    b.ToTable("TicketDetails");
                });

            modelBuilder.Entity("ElysSalon2._0.Core.domain.Entities.Article", b =>
                {
                    b.HasOne("ElysSalon2._0.Core.domain.Entities.ArticleType", "ArticleType")
                        .WithMany()
                        .HasForeignKey("ArticleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArticleType");
                });

            modelBuilder.Entity("ElysSalon2._0.Core.domain.Entities.TicketDetails", b =>
                {
                    b.HasOne("ElysSalon2._0.Core.domain.Entities.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElysSalon2._0.Core.domain.Entities.Ticket", "Ticket")
                        .WithMany("TicketDetails")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("ElysSalon2._0.Core.domain.Entities.Ticket", b =>
                {
                    b.Navigation("TicketDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
