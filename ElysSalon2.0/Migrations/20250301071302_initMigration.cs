using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class initMigration : Migration
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
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmissionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Issuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalOutTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWithTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
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
                    PriceCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                table: "Article",
                columns: new[] { "ArticleId", "ArticleTypeId", "Description", "Name", "PriceBuy", "PriceCost", "Stock" },
                values: new object[,]
                {
                    { 1, 2, "MEESI", "Tinte", 12.59m, 2.5m, 10 },
                    { 2, 3, "MEESI", "Uñas Acrilicas", 22.59m, 2.5m, 1 },
                    { 3, 3, "MEESI", "Pedicure", 32.59m, 2.5m, 1 },
                    { 4, 3, "MEESI", "Manicure", 52.59m, 1.5m, 1 },
                    { 5, 3, "MEESI", "Corte Hombre", 5.5m, 2.5m, 1 },
                    { 6, 3, "MEESI", "Corte Mujer", 7.59m, 3.5m, 1 },
                    { 7, 5, "MEESI", "Aritos", 32.59m, 2.5m, 15 },
                    { 8, 5, "MEESI", "Pestañas", 52.59m, 1.5m, 10 },
                    { 9, 3, "MEESI", "Depilado Cejas", 42.59m, 1.6m, 1 },
                    { 10, 2, "MEESI", "Kersel", 65.59m, 11.6m, 21 }
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
