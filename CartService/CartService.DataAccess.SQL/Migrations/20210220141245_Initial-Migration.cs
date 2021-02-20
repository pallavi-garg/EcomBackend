using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CartService.DataAccess.SQL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    CustomerId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartProductMapping",
                columns: table => new
                {
                    CartId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SKU = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Id = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProductMapping", x => new { x.CartId, x.ProductId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "CartProductMapping");
        }
    }
}
