using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class dataseeds_created : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SaleId", "SaleDate", "Total" },
                values: new object[,]
                {
                    { 100, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.50m },
                    { 101, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 59.30m },
                    { 102, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.50m },
                    { 103, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 23.70m },
                    { 104, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 56.40m },
                    { 105, new DateTime(2025, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.00m },
                    { 106, new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 65.10m },
                    { 107, new DateTime(2025, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 13.30m },
                    { 108, new DateTime(2025, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 27.00m },
                    { 109, new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 34.90m },
                    { 110, new DateTime(2025, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 80.10m },
                    { 111, new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 28.20m },
                    { 112, new DateTime(2025, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 77.40m },
                    { 113, new DateTime(2025, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 66.90m },
                    { 114, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 97.50m },
                    { 115, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 69.90m },
                    { 116, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 66.45m },
                    { 117, new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.10m },
                    { 118, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.40m },
                    { 119, new DateTime(2025, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 49.40m },
                    { 120, new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 38.50m },
                    { 122, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 80.10m },
                    { 123, new DateTime(2025, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 110.40m },
                    { 124, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 119.40m },
                    { 125, new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 63.50m },
                    { 126, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 57.42m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 126);

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SaleId", "SaleDate", "Total" },
                values: new object[,]
                {
                    { 200, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 20.50m },
                    { 201, new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 59.30m },
                    { 202, new DateTime(2025, 3, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.50m },
                    { 203, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 23.70m },
                    { 204, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 56.40m },
                    { 205, new DateTime(2025, 3, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.00m },
                    { 206, new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 65.10m },
                    { 207, new DateTime(2025, 3, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 13.30m },
                    { 208, new DateTime(2025, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 27.00m },
                    { 209, new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 34.90m },
                    { 210, new DateTime(2025, 3, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 80.10m },
                    { 211, new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 28.20m },
                    { 212, new DateTime(2025, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 77.40m },
                    { 213, new DateTime(2025, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 66.90m },
                    { 214, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 97.50m },
                    { 215, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 69.90m },
                    { 216, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 66.45m },
                    { 217, new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.10m },
                    { 218, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.40m },
                    { 219, new DateTime(2025, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 49.40m },
                    { 220, new DateTime(2025, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 38.50m },
                    { 222, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 80.10m },
                    { 223, new DateTime(2025, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 110.40m },
                    { 224, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 119.40m },
                    { 225, new DateTime(2025, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 63.50m },
                    { 226, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 57.42m }
                });
        }
    }
}
