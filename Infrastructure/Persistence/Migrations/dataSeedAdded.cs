#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class dataSeedAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            422,
            "Date",
            new DateTime(2025, 5, 14, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            423,
            "Date",
            new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            424,
            "Date",
            new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            425,
            "Date",
            new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            426,
            "Date",
            new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            427,
            "Date",
            new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            428,
            "Date",
            new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            429,
            "Date",
            new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            430,
            "Date",
            new DateTime(2025, 5, 7, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            431,
            "Date",
            new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            432,
            "Date",
            new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Local));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            422,
            "Date",
            new DateTime(2025, 5, 13, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            423,
            "Date",
            new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            424,
            "Date",
            new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            425,
            "Date",
            new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            426,
            "Date",
            new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            427,
            "Date",
            new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            428,
            "Date",
            new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            429,
            "Date",
            new DateTime(2025, 5, 11, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            430,
            "Date",
            new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            431,
            "Date",
            new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            432,
            "Date",
            new DateTime(2025, 3, 13, 0, 0, 0, 0, DateTimeKind.Local));
    }
}