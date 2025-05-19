using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class dataSeed_to_ticketdetails3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TicketDetails",
                columns: new[] { "TicketDetailsId", "ArticleId", "ArticleName", "Date", "Price", "Quantity", "TicketId" },
                values: new object[,]
                {
                    { 428, 4, "Desde hace 3 meses", new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Local), 12.59m, 1, "001100" },
                    { 429, 9, "Desde hace 3 meses", new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Local), 12.59m, 1, "001100" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 428);

            migrationBuilder.DeleteData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 429);
        }
    }
}
