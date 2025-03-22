using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class tickets_seeds_created : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "EmissionDateTime", "Issuer", "TotalAmount", "TotalOutTaxes", "TotalWithTaxes" },
                values: new object[,]
                {
                    { "000100", new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 50.00m, null, null },
                    { "000101", new DateTime(2025, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 50.00m, null, null },
                    { "000102", new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 100.00m, null, null },
                    { "000103", new DateTime(2025, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 100.00m, null, null },
                    { "000104", new DateTime(2025, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 200.00m, null, null },
                    { "000105", new DateTime(2025, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 200.00m, null, null },
                    { "000106", new DateTime(2025, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 400.00m, null, null },
                    { "000107", new DateTime(2025, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 400.00m, null, null },
                    { "000108", new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 800.00m, null, null },
                    { "000109", new DateTime(2025, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 800.00m, null, null },
                    { "000110", new DateTime(2025, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1600.00m, null, null },
                    { "000111", new DateTime(2025, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1600.00m, null, null },
                    { "000112", new DateTime(2025, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3200.00m, null, null },
                    { "000113", new DateTime(2025, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 3200.00m, null, null },
                    { "000114", new DateTime(2025, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 6400.00m, null, null },
                    { "000115", new DateTime(2025, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 6400.00m, null, null },
                    { "000116", new DateTime(2025, 2, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 128000.00m, null, null },
                    { "000117", new DateTime(2025, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 128000.00m, null, null },
                    { "000118", new DateTime(2025, 3, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 256000.00m, null, null },
                    { "000119", new DateTime(2025, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 256000.00m, null, null },
                    { "000120", new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 512000.00m, null, null },
                    { "000121", new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 512000.00m, null, null },
                    { "000122", new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 600000.00m, null, null },
                    { "000123", new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 600000.00m, null, null },
                    { "000124", new DateTime(2025, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 800000.00m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000100");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000101");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000102");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000103");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000104");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000105");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000106");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000107");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000108");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000109");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000110");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000111");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000112");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000113");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000114");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000115");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000116");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000117");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000118");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000119");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000120");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000121");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000122");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000123");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "000124");
        }
    }
}
