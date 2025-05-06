using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class new_data_seeds_to_tickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "TicketId", "EmissionDateTime", "Issuer", "TotalAmount", "TotalOutTaxes", "TotalWithTaxes" },
                values: new object[,]
                {
                    { "001133", new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 24.80m, null, null },
                    { "001134", new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 12.60m, null, null },
                    { "001135", new DateTime(2025, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 22.40m, null, null },
                    { "001136", new DateTime(2025, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 34.50m, null, null },
                    { "001137", new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 34.50m, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "001133");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "001134");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "001135");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "001136");

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "TicketId",
                keyValue: "001137");
        }
    }
}
