using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class dataSeed_to_Expenses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Expense",
                columns: new[] { "Id", "Amount", "Date", "Reason" },
                values: new object[,]
                {
                    { 1, 25.50m, new DateTime(2025, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Compra de suministros" },
                    { 2, 15.75m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Almuerzo" },
                    { 3, 80.00m, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Combustible" },
                    { 4, 35.20m, new DateTime(2025, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reparación menor" },
                    { 5, 12.99m, new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suscripción online" },
                    { 6, 210.50m, new DateTime(2025, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Viaje de negocios" },
                    { 7, 45.00m, new DateTime(2025, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Material de oficina" },
                    { 8, 65.80m, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pago de internet" },
                    { 9, 1200.00m, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alquiler de local" },
                    { 10, 90.30m, new DateTime(2025, 3, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Curso online" },
                    { 11, 18.75m, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mantenimiento web" },
                    { 12, 75.00m, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Publicidad Facebook" },
                    { 13, 5.50m, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Impresiones" },
                    { 14, 30.25m, new DateTime(2025, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Productos de limpieza" },
                    { 15, 40.00m, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Soporte técnico" },
                    { 16, 150.00m, new DateTime(2025, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Licencia de software" },
                    { 17, 55.60m, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seguro" },
                    { 18, 12.30m, new DateTime(2025, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Envío de documentos" },
                    { 19, 300.00m, new DateTime(2025, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consultoría" },
                    { 20, 85.40m, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Entrada a evento" },
                    { 21, 28.90m, new DateTime(2025, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Suministros varios" },
                    { 22, 18.50m, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cafetería" },
                    { 23, 95.00m, new DateTime(2025, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mantenimiento vehículo" },
                    { 24, 42.15m, new DateTime(2025, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Arreglo de oficina" },
                    { 25, 7.99m, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "App de productividad" },
                    { 26, 180.75m, new DateTime(2025, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alojamiento" },
                    { 27, 38.20m, new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Papelería" },
                    { 28, 70.50m, new DateTime(2025, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pago de agua" },
                    { 29, 1100.00m, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Publicidad impresa" },
                    { 30, 105.60m, new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Seminario" },
                    { 31, 22.40m, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Actualización web" },
                    { 32, 88.00m, new DateTime(2025, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Google Ads" },
                    { 33, 9.20m, new DateTime(2025, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fotocopias" },
                    { 34, 33.90m, new DateTime(2025, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Material de aseo" },
                    { 35, 58.15m, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Repuestos" },
                    { 36, 165.00m, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Software contable" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Expense",
                keyColumn: "Id",
                keyValue: 36);
        }
    }
}
