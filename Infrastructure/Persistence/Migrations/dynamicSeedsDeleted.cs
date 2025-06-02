using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class dynamicSeedsDeleted : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            422,
            "Date",
            new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            423,
            "Date",
            new DateTime(2025, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            424,
            "Date",
            new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            425,
            "Date",
            new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            426,
            "Date",
            new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            427,
            "Date",
            new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            428,
            "Date",
            new DateTime(2025, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            431,
            "Date",
            new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            432,
            "Date",
            new DateTime(2025, 5, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            422,
            "Date",
            new DateTime(2025, 5, 27, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            423,
            "Date",
            new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            424,
            "Date",
            new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            425,
            "Date",
            new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            426,
            "Date",
            new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            427,
            "Date",
            new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            428,
            "Date",
            new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            431,
            "Date",
            new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Local));

        migrationBuilder.UpdateData(
            "TicketDetails",
            "TicketDetailsId",
            432,
            "Date",
            new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Local));
    }
}