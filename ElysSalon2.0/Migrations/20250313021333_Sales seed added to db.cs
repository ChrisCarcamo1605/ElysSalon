using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class Salesseedaddedtodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sale");

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "SaleId", "SaleDate", "Total" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.0 },
                    { 2, new DateTime(2023, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 75.5 },
                    { 3, new DateTime(2023, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 120.25 },
                    { 4, new DateTime(2023, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.75 },
                    { 5, new DateTime(2023, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 90.0 },
                    { 6, new DateTime(2023, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 60.0 },
                    { 7, new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 85.5 },
                    { 8, new DateTime(2023, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 130.25 },
                    { 9, new DateTime(2023, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 40.75 },
                    { 10, new DateTime(2023, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 100.0 },
                    { 11, new DateTime(2023, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 70.0 },
                    { 12, new DateTime(2023, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 95.5 },
                    { 13, new DateTime(2023, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 140.25 },
                    { 14, new DateTime(2023, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.75 },
                    { 15, new DateTime(2023, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 110.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Sales",
                keyColumn: "SaleId",
                keyValue: 15);

            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    SaleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.SaleId);
                });
        }
    }
}
