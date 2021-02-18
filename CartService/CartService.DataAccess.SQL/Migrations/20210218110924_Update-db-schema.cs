using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CartService.DataAccess.SQL.Migrations
{
    public partial class Updatedbschema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("325c5ee8-ba74-47c5-9e4c-a8bd24a9b570"));

            migrationBuilder.DeleteData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("8fa73bc7-fe97-4c67-9ac3-2991958f6469"));

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartProductMapping",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ModifiedDate" },
                values: new object[] { new Guid("56bf938d-f7a5-42fc-a9d8-6b6a9172ba7a"), new DateTime(2021, 2, 18, 12, 9, 24, 490, DateTimeKind.Local).AddTicks(4610), "RAHULXXXX123", new DateTime(2021, 2, 18, 12, 9, 24, 490, DateTimeKind.Local).AddTicks(7301) });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ModifiedDate" },
                values: new object[] { new Guid("dd5cb491-0261-49a9-9171-32dff07b5c25"), new DateTime(2021, 4, 9, 12, 9, 24, 490, DateTimeKind.Local).AddTicks(8134), "RAHULXXXX123", new DateTime(2021, 4, 9, 12, 9, 24, 490, DateTimeKind.Local).AddTicks(8207) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("56bf938d-f7a5-42fc-a9d8-6b6a9172ba7a"));

            migrationBuilder.DeleteData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("dd5cb491-0261-49a9-9171-32dff07b5c25"));

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartProductMapping");

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ModifiedDate" },
                values: new object[] { new Guid("325c5ee8-ba74-47c5-9e4c-a8bd24a9b570"), new DateTime(2021, 2, 14, 15, 21, 57, 806, DateTimeKind.Local).AddTicks(6476), "RAHULXXXX123", new DateTime(2021, 2, 14, 15, 21, 57, 806, DateTimeKind.Local).AddTicks(8462) });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ModifiedDate" },
                values: new object[] { new Guid("8fa73bc7-fe97-4c67-9ac3-2991958f6469"), new DateTime(2021, 4, 5, 15, 21, 57, 806, DateTimeKind.Local).AddTicks(9027), "RAHULXXXX123", new DateTime(2021, 4, 5, 15, 21, 57, 806, DateTimeKind.Local).AddTicks(9075) });
        }
    }
}
