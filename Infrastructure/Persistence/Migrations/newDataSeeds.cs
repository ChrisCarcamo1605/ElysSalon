#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class newDataSeeds : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            "Expense",
            "Id",
            9,
            "Amount",
            200.00m);

        migrationBuilder.UpdateData(
            "Expense",
            "Id",
            29,
            "Amount",
            100.00m);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            "Expense",
            "Id",
            9,
            "Amount",
            1200.00m);

        migrationBuilder.UpdateData(
            "Expense",
            "Id",
            29,
            "Amount",
            1100.00m);
    }
}