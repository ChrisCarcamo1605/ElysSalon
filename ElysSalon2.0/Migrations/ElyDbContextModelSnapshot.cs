﻿// <auto-generated />
using System;
using ElysSalon2._0.adapters.OutBound.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ElysSalon2._0.Migrations
{
    [DbContext(typeof(ElyDbContext))]
    partial class ElyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ElysSalon2._0.domain.Entities.Article", b =>
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

            modelBuilder.Entity("ElysSalon2._0.domain.Entities.ArticleType", b =>
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

            modelBuilder.Entity("ElysSalon2._0.domain.Entities.Ticket", b =>
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

                    b.Property<decimal>("TotalOutTaxes")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalWithTaxes")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TicketId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("ElysSalon2._0.domain.Entities.TicketDetails", b =>
                {
                    b.Property<int>("TicketDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TicketDetailsId"));

                    b.Property<int>("ArticleId")
                        .HasColumnType("int");

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

            modelBuilder.Entity("ElysSalon2._0.domain.Entities.Article", b =>
                {
                    b.HasOne("ElysSalon2._0.domain.Entities.ArticleType", "ArticleType")
                        .WithMany()
                        .HasForeignKey("ArticleTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArticleType");
                });

            modelBuilder.Entity("ElysSalon2._0.domain.Entities.TicketDetails", b =>
                {
                    b.HasOne("ElysSalon2._0.domain.Entities.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ElysSalon2._0.domain.Entities.Ticket", "Ticket")
                        .WithMany("TicketDetails")
                        .HasForeignKey("TicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("ElysSalon2._0.domain.Entities.Ticket", b =>
                {
                    b.Navigation("TicketDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
