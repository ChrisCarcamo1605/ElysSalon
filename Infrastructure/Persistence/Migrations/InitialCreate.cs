﻿#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "ArticleType",
            table => new
            {
                ArticleTypeId = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_ArticleType", x => x.ArticleTypeId); });

        migrationBuilder.CreateTable(
            "Sales",
            table => new
            {
                SaleId = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SaleDate = table.Column<DateTime>("datetime2", nullable: false),
                Total = table.Column<decimal>("decimal(18,2)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Sales", x => x.SaleId); });

        migrationBuilder.CreateTable(
            "Tickets",
            table => new
            {
                TicketId = table.Column<string>("nvarchar(450)", nullable: false),
                EmissionDateTime = table.Column<DateTime>("datetime2", nullable: false),
                Issuer = table.Column<string>("nvarchar(max)", nullable: false),
                TotalOutTaxes = table.Column<decimal>("decimal(18,2)", nullable: true),
                TotalWithTaxes = table.Column<decimal>("decimal(18,2)", nullable: true),
                TotalAmount = table.Column<decimal>("decimal(18,2)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Tickets", x => x.TicketId); });

        migrationBuilder.CreateTable(
            "Article",
            table => new
            {
                ArticleId = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                ArticleTypeId = table.Column<int>("int", nullable: false),
                PriceCost = table.Column<decimal>("decimal(18,2)", nullable: false),
                PriceBuy = table.Column<decimal>("decimal(18,2)", nullable: false),
                Stock = table.Column<int>("int", nullable: false),
                Description = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Article", x => x.ArticleId);
                table.ForeignKey(
                    "FK_Article_ArticleType_ArticleTypeId",
                    x => x.ArticleTypeId,
                    "ArticleType",
                    "ArticleTypeId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "TicketDetails",
            table => new
            {
                TicketDetailsId = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TicketId = table.Column<string>("nvarchar(450)", nullable: false),
                ArticleName = table.Column<string>("nvarchar(max)", nullable: false),
                ArticleId = table.Column<int>("int", nullable: false),
                Quantity = table.Column<int>("int", nullable: false),
                Price = table.Column<decimal>("decimal(18,2)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TicketDetails", x => x.TicketDetailsId);
                table.ForeignKey(
                    "FK_TicketDetails_Article_ArticleId",
                    x => x.ArticleId,
                    "Article",
                    "ArticleId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_TicketDetails_Tickets_TicketId",
                    x => x.TicketId,
                    "Tickets",
                    "TicketId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            "ArticleType",
            new[] { "ArticleTypeId", "Name" },
            new object[,]
            {
                { 1, "Todo" },
                { 2, "Elegir Tipo" },
                { 3, "Cabello" },
                { 4, "Servicio" },
                { 5, "Tintes" },
                { 6, "Producto" }
            });

        migrationBuilder.InsertData(
            "Sales",
            new[] { "SaleId", "SaleDate", "Total" },
            new object[,]
            {
                { 100, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.50m },
                { 101, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 59.30m },
                { 102, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.50m },
                { 103, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 23.70m },
                { 104, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 56.40m },
                { 105, new DateTime(2025, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.00m },
                { 106, new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 65.10m },
                { 107, new DateTime(2025, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 13.30m },
                { 108, new DateTime(2025, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 27.00m },
                { 109, new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 34.90m },
                { 110, new DateTime(2025, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 80.10m },
                { 111, new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 28.20m },
                { 112, new DateTime(2025, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 77.40m },
                { 113, new DateTime(2025, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 66.90m },
                { 114, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 97.50m },
                { 115, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 69.90m },
                { 116, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 66.45m },
                { 117, new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.10m },
                { 118, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.40m },
                { 119, new DateTime(2025, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 49.40m },
                { 120, new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 38.50m },
                { 122, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 80.10m },
                { 123, new DateTime(2025, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 110.40m },
                { 124, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 119.40m },
                { 125, new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 63.50m },
                { 126, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 57.42m },
                { 127, new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.40m },
                { 128, new DateTime(2025, 4, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 49.40m },
                { 129, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 38.50m },
                { 130, new DateTime(2025, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 80.10m },
                { 131, new DateTime(2025, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 110.40m },
                { 132, new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 119.40m },
                { 133, new DateTime(2025, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 63.50m }
            });

        migrationBuilder.InsertData(
            "Tickets",
            new[] { "TicketId", "EmissionDateTime", "Issuer", "TotalAmount", "TotalOutTaxes", "TotalWithTaxes" },
            new object[,]
            {
                { "001100", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 18.50m, null, null },
                { "001101", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 22.30m, null, null },
                { "001102", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 19.70m, null, null },
                { "001103", new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 24.10m, null, null },
                { "001104", new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 25.20m, null, null },
                { "001105", new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 30.50m, null, null },
                { "001106", new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 27.80m, null, null },
                { "001107", new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 15.90m, null, null },
                { "001108", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 36.40m, null, null },
                { "001109", new DateTime(2025, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 22.10m, null, null },
                { "001110", new DateTime(2025, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 17.90m, null, null },
                { "001111", new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 35.10m, null, null },
                { "001112", new DateTime(2025, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 23.30m, null, null },
                { "001113", new DateTime(2025, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 31.25m, null, null },
                { "001114", new DateTime(2025, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 25.75m, null, null },
                { "001115", new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 38.90m, null, null },
                { "001116", new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 18.60m, null, null },
                { "001117", new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 17.40m, null, null },
                { "001118", new DateTime(2025, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 16.10m, null, null },
                { "001119", new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 12.30m, null, null },
                { "001120", new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 31.90m, null, null },
                { "001121", new DateTime(2025, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 11.60m, null, null },
                { "001122", new DateTime(2025, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 21.80m, null, null },
                { "001123", new DateTime(2025, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 11.90m, null, null },
                { "001124", new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 21.50m, null, null },
                { "001125", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 21.80m, null, null },
                { "001126", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 42.10m, null, null },
                { "001127", new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 36.45m, null, null },
                { "001128", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 15.10m, null, null },
                { "001129", new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 24.80m, null, null },
                { "001130", new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 12.60m, null, null },
                { "001131", new DateTime(2025, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 22.40m, null, null },
                { "001132", new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 34.50m, null, null }
            });

        migrationBuilder.InsertData(
            "Article",
            new[] { "ArticleId", "ArticleTypeId", "Description", "Name", "PriceBuy", "PriceCost", "Stock" },
            new object[,]
            {
                { 1, 5, "MEESI", "Tinte Rojo", 12.59m, 2.5m, 10 },
                { 2, 4, "MEESI", "Uñas Acrilicas", 22.59m, 2.5m, 1 },
                { 3, 4, "MEESI", "Pedicure", 32.59m, 2.5m, 1 },
                { 4, 4, "MEESI", "Manicure", 52.59m, 1.5m, 1 },
                { 5, 4, "MEESI", "Corte Hombre", 5.5m, 2.5m, 1 },
                { 6, 4, "MEESI", "Corte Mujer", 7.59m, 3.5m, 1 },
                { 7, 5, "MEESI", "Aritos", 32.59m, 2.5m, 15 },
                { 8, 5, "MEESI", "Pestañas", 52.59m, 1.5m, 10 },
                { 9, 3, "MEESI", "Depilado Cejas", 42.59m, 1.6m, 1 },
                { 10, 3, "MEESI", "Kersel", 65.59m, 11.6m, 21 }
            });

        migrationBuilder.CreateIndex(
            "IX_Article_ArticleTypeId",
            "Article",
            "ArticleTypeId");

        migrationBuilder.CreateIndex(
            "IX_TicketDetails_ArticleId",
            "TicketDetails",
            "ArticleId");

        migrationBuilder.CreateIndex(
            "IX_TicketDetails_TicketId",
            "TicketDetails",
            "TicketId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Sales");

        migrationBuilder.DropTable(
            "TicketDetails");

        migrationBuilder.DropTable(
            "Article");

        migrationBuilder.DropTable(
            "Tickets");

        migrationBuilder.DropTable(
            "ArticleType");
    }
}