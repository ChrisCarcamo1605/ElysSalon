#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class dataSeedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 422,
                column: "Date",
                value: new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 423,
                column: "Date",
                value: new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 424,
                column: "Date",
                value: new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 425,
                column: "Date",
                value: new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 426,
                column: "Date",
                value: new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 427,
                column: "Date",
                value: new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 428,
                column: "Date",
                value: new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 429,
                column: "Date",
                value: new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 430,
                column: "Date",
                value: new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 431,
                column: "Date",
                value: new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 432,
                column: "Date",
                value: new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 422,
                column: "Date",
                value: new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 423,
                column: "Date",
                value: new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 424,
                column: "Date",
                value: new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 425,
                column: "Date",
                value: new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 426,
                column: "Date",
                value: new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 427,
                column: "Date",
                value: new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 428,
                column: "Date",
                value: new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 429,
                column: "Date",
                value: new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 430,
                column: "Date",
                value: new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 431,
                column: "Date",
                value: new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "TicketDetails",
                keyColumn: "TicketDetailsId",
                keyValue: 432,
                column: "Date",
                value: new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
