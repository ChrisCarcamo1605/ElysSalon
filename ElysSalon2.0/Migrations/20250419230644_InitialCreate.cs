using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleType",
                columns: table => new
                {
                    ArticleTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleType", x => x.ArticleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.SaleId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmissionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalOutTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalWithTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ArticleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleTypeId = table.Column<int>(type: "int", nullable: false),
                    PriceCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceBuy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleId);
                    table.ForeignKey(
                        name: "FK_Article_ArticleType_ArticleTypeId",
                        column: x => x.ArticleTypeId,
                        principalTable: "ArticleType",
                        principalColumn: "ArticleTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketDetails",
                columns: table => new
                {
                    TicketDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ArticleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDetails", x => x.TicketDetailsId);
                    table.ForeignKey(
                        name: "FK_TicketDetails_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "ArticleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketDetails_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ArticleType",
                columns: new[] { "ArticleTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Todo" },
                    { 2, "Elegir Tipo" },
                    { 3, "Cabello" },
                    { 4, "Servicio" },
                    { 5, "Tintes" },
                    { 6, "Producto" }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SaleId", "SaleDate", "Total" },
                values: new object[,]
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
                table: "Tickets",
                columns: new[] { "TicketId", "EmissionDateTime", "Issuer", "TotalAmount", "TotalOutTaxes", "TotalWithTaxes" },
                values: new object[,]
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
                table: "Article",
                columns: new[] { "ArticleId", "ArticleTypeId", "Description", "Name", "PriceBuy", "PriceCost", "Stock" },
                values: new object[,]
                {
                    { 1, 5, "MEESI", "Tinte Rojo", 12.59m, 2.5m, 10 },
                    { 2, 3, "MEESI", "Uñas Acrilicas", 22.59m, 2.5m, 1 },
                    { 3, 3, "MEESI", "Pedicure", 32.59m, 2.5m, 1 },
                    { 4, 3, "MEESI", "Manicure", 52.59m, 1.5m, 1 },
                    { 5, 3, "MEESI", "Corte Hombre", 5.5m, 2.5m, 1 },
                    { 6, 3, "MEESI", "Corte Mujer", 7.59m, 3.5m, 1 },
                    { 7, 5, "MEESI", "Aritos", 32.59m, 2.5m, 15 },
                    { 8, 5, "MEESI", "Pestañas", 52.59m, 1.5m, 10 },
                    { 9, 3, "MEESI", "Depilado Cejas", 42.59m, 1.6m, 1 },
                    { 10, 3, "MEESI", "Kersel", 65.59m, 11.6m, 21 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_ArticleTypeId",
                table: "Article",
                column: "ArticleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_ArticleId",
                table: "TicketDetails",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_TicketId",
                table: "TicketDetails",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "TicketDetails");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "ArticleType");
        }
    }
}
