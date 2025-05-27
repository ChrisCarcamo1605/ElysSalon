using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dynamicSeedsDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 422,
                column: "Date",
                value: new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 423,
                column: "Date",
                value: new DateTime(2025, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 424,
                column: "Date",
                value: new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 425,
                column: "Date",
                value: new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 426,
                column: "Date",
                value: new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 427,
                column: "Date",
                value: new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 428,
                column: "Date",
                value: new DateTime(2025, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 431,
                column: "Date",
                value: new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 432,
                column: "Date",
                value: new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 422,
                column: "Date",
                value: new DateTime(2025, 5, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 423,
                column: "Date",
                value: new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 424,
                column: "Date",
                value: new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 425,
                column: "Date",
                value: new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 426,
                column: "Date",
                value: new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 427,
                column: "Date",
                value: new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 428,
                column: "Date",
                value: new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 431,
                column: "Date",
                value: new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 432,
                column: "Date",
                value: new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
