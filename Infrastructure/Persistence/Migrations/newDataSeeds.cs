#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newDataSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 9,
                column: "Amount",
                value: 200.00m);

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 29,
                column: "Amount",
                value: 100.00m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 9,
                column: "Amount",
                value: 1200.00m);

            migrationBuilder.UpdateData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 29,
                column: "Amount",
                value: 1100.00m);
        }
    }
}
