using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElysSalon2._0.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleTypes",
                columns: table => new
                {
                    articleTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTypes", x => x.articleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ticketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    emissionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    issuer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalOutTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    totalWithTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    totalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ticketId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ticketId);
                    table.ForeignKey(
                        name: "FK_Tickets_Tickets_ticketId1",
                        column: x => x.ticketId1,
                        principalTable: "Tickets",
                        principalColumn: "ticketId");
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    articleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    articleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    articleTypeId = table.Column<int>(type: "int", nullable: false),
                    priceCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    priceBuy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    articleId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.articleId);
                    table.ForeignKey(
                        name: "FK_Articles_ArticleTypes_articleTypeId",
                        column: x => x.articleTypeId,
                        principalTable: "ArticleTypes",
                        principalColumn: "articleTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Articles_Articles_articleId1",
                        column: x => x.articleId1,
                        principalTable: "Articles",
                        principalColumn: "articleId");
                });

            migrationBuilder.CreateTable(
                name: "TicketDetails",
                columns: table => new
                {
                    ticketDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ticketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    articleId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    totalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ticketDetailsId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDetails", x => x.ticketDetailsId);
                    table.ForeignKey(
                        name: "FK_TicketDetails_Articles_articleId",
                        column: x => x.articleId,
                        principalTable: "Articles",
                        principalColumn: "articleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketDetails_TicketDetails_ticketDetailsId1",
                        column: x => x.ticketDetailsId1,
                        principalTable: "TicketDetails",
                        principalColumn: "ticketDetailsId");
                    table.ForeignKey(
                        name: "FK_TicketDetails_Tickets_ticketId",
                        column: x => x.ticketId,
                        principalTable: "Tickets",
                        principalColumn: "ticketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_articleId1",
                table: "Articles",
                column: "articleId1");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_articleTypeId",
                table: "Articles",
                column: "articleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_articleId",
                table: "TicketDetails",
                column: "articleId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_ticketDetailsId1",
                table: "TicketDetails",
                column: "ticketDetailsId1");

            migrationBuilder.CreateIndex(
                name: "IX_TicketDetails_ticketId",
                table: "TicketDetails",
                column: "ticketId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ticketId1",
                table: "Tickets",
                column: "ticketId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketDetails");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "ArticleTypes");
        }
    }
}
