using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CartService.DataAccess.SQL.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(900)", nullable: false),
                    CustomerId = table.Column<string>(type: "varchar(900)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                    table.UniqueConstraint("Unique_CustomerId", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CartProductMapping",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(900)", nullable: false),
                    CartId = table.Column<string>(type: "varchar(900)", nullable: false),
                    ProductId = table.Column<string>(type: "varchar(900)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProductMapping", x => new { x.CartId, x.ProductId });
                    table.ForeignKey("FK_CartProductMapping", x => x.CartId, "Cart", "Id" ,null, ReferentialAction.NoAction, ReferentialAction.Cascade);
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
