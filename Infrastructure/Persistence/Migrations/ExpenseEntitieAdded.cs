#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class ExpenseEntitieAdded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Expense",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Amount = table.Column<decimal>("decimal(18,2)", nullable: false),
                Reason = table.Column<string>("nvarchar(max)", nullable: false),
                Date = table.Column<DateTime>("datetime2", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_Expense", x => x.Id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Expense");
    }
}