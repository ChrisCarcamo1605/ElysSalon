using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class newDataSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "ArticleId",
                keyValue: 1,
                columns: new[] { "ArticleTypeId", "Name" },
                values: new object[] { 5, "Tinte Rojo" });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "ArticleId",
                keyValue: 10,
                column: "ArticleTypeId",
                value: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "ArticleId",
                keyValue: 1,
                columns: new[] { "ArticleTypeId", "Name" },
                values: new object[] { 2, "Tinte" });

            migrationBuilder.UpdateData(
                table: "Article",
                keyColumn: "ArticleId",
                keyValue: 10,
                column: "ArticleTypeId",
                value: 2);
        }
    }
}
