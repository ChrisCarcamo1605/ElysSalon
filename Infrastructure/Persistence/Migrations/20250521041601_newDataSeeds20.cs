using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newDataSeeds20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 422,
                column: "Date",
                value: new DateTime(2025, 5, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 423,
                column: "Date",
                value: new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 424,
                column: "Date",
                value: new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 425,
                column: "Date",
                value: new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 426,
                column: "Date",
                value: new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 427,
                column: "Date",
                value: new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 428,
                column: "Date",
                value: new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 429,
                column: "Date",
                value: new DateTime(2025, 5, 18, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 430,
                column: "Date",
                value: new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 431,
                column: "Date",
                value: new DateTime(2025, 5, 8, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 432,
                column: "Date",
                value: new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 422,
                column: "Date",
                value: new DateTime(2025, 5, 18, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 423,
                column: "Date",
                value: new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 424,
                column: "Date",
                value: new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 425,
                column: "Date",
                value: new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 426,
                column: "Date",
                value: new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 427,
                column: "Date",
                value: new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 428,
                column: "Date",
                value: new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 429,
                column: "Date",
                value: new DateTime(2025, 5, 16, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 430,
                column: "Date",
                value: new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 431,
                column: "Date",
                value: new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 432,
                column: "Date",
                value: new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
