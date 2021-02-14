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
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CartProductMapping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProductMapping", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ModifiedDate" },
                values: new object[] { new Guid("325c5ee8-ba74-47c5-9e4c-a8bd24a9b570"), new DateTime(2021, 2, 14, 15, 21, 57, 806, DateTimeKind.Local).AddTicks(6476), "RAHULXXXX123", new DateTime(2021, 2, 14, 15, 21, 57, 806, DateTimeKind.Local).AddTicks(8462) });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ModifiedDate" },
                values: new object[] { new Guid("8fa73bc7-fe97-4c67-9ac3-2991958f6469"), new DateTime(2021, 4, 5, 15, 21, 57, 806, DateTimeKind.Local).AddTicks(9027), "RAHULXXXX123", new DateTime(2021, 4, 5, 15, 21, 57, 806, DateTimeKind.Local).AddTicks(9075) });
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
