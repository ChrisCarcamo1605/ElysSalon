using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class newseedsrefactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 11,
                column: "SaleDate",
                value: new DateTime(2025, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 12,
                column: "SaleDate",
                value: new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 13,
                column: "SaleDate",
                value: new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 14,
                column: "SaleDate",
                value: new DateTime(2025, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 15,
                column: "SaleDate",
                value: new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 11,
                column: "SaleDate",
                value: new DateTime(2023, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 12,
                column: "SaleDate",
                value: new DateTime(2023, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 13,
                column: "SaleDate",
                value: new DateTime(2023, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 14,
                column: "SaleDate",
                value: new DateTime(2023, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 15,
                column: "SaleDate",
                value: new DateTime(2023, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
