using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CartService.DataAccess.SQL.Migrations
{
    public partial class Updatemappingdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("56bf938d-f7a5-42fc-a9d8-6b6a9172ba7a"));

            migrationBuilder.DeleteData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("dd5cb491-0261-49a9-9171-32dff07b5c25"));

            migrationBuilder.AddColumn<string>(
                name: "CartId",
                table: "Cart",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CartId", "CreatedAt", "CustomerId", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("523e5163-8640-47b0-b511-ff61b29effdc"), null, new DateTime(2021, 2, 18, 13, 31, 4, 244, DateTimeKind.Local).AddTicks(2395), "RAHULXXXX123", new DateTime(2021, 2, 18, 13, 31, 4, 244, DateTimeKind.Local).AddTicks(4512) },
                    { new Guid("65c941a5-af82-42bb-80dd-c81a5a73c0b5"), null, new DateTime(2021, 4, 9, 13, 31, 4, 244, DateTimeKind.Local).AddTicks(5076), "RAHULXXXX123", new DateTime(2021, 4, 9, 13, 31, 4, 244, DateTimeKind.Local).AddTicks(5125) }
                });

            migrationBuilder.InsertData(
                table: "CartProductMapping",
                columns: new[] { "Id", "CartId", "CreatedAt", "ModifiedDate", "ProductId", "Quantity", "SKU" },
                values: new object[,]
                {
                    { new Guid("12f9d48c-d899-4db7-a229-cbe823695ca0"), "8FA73BC7-FE97-4C67-9AC3-2991958F6469", new DateTime(2021, 2, 18, 13, 31, 4, 246, DateTimeKind.Local).AddTicks(30), new DateTime(2021, 2, 18, 13, 31, 4, 246, DateTimeKind.Local).AddTicks(42), "XYZ_12345", 2, "ADCSKKK_SSW#$%&**SS^&*()" },
                    { new Guid("77cfb577-59a4-4a89-a9f0-bef5a6f9efb9"), "325C5EE8-BA74-47C5-9E4C-A8BD24A9B570", new DateTime(2021, 2, 18, 13, 31, 4, 246, DateTimeKind.Local).AddTicks(1704), new DateTime(2021, 2, 18, 13, 31, 4, 246, DateTimeKind.Local).AddTicks(1708), "ABCD_XXXX", 2, "ADCSKKK_SSWqweeqweqweqwe" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("523e5163-8640-47b0-b511-ff61b29effdc"));

            migrationBuilder.DeleteData(
                table: "Cart",
                keyColumn: "Id",
                keyValue: new Guid("65c941a5-af82-42bb-80dd-c81a5a73c0b5"));

            migrationBuilder.DeleteData(
                table: "CartProductMapping",
                keyColumn: "Id",
                keyValue: new Guid("12f9d48c-d899-4db7-a229-cbe823695ca0"));

            migrationBuilder.DeleteData(
                table: "CartProductMapping",
                keyColumn: "Id",
                keyValue: new Guid("77cfb577-59a4-4a89-a9f0-bef5a6f9efb9"));

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Cart");

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ModifiedDate" },
                values: new object[] { new Guid("56bf938d-f7a5-42fc-a9d8-6b6a9172ba7a"), new DateTime(2021, 2, 18, 12, 9, 24, 490, DateTimeKind.Local).AddTicks(4610), "RAHULXXXX123", new DateTime(2021, 2, 18, 12, 9, 24, 490, DateTimeKind.Local).AddTicks(7301) });

            migrationBuilder.InsertData(
                table: "Cart",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "ModifiedDate" },
                values: new object[] { new Guid("dd5cb491-0261-49a9-9171-32dff07b5c25"), new DateTime(2021, 4, 9, 12, 9, 24, 490, DateTimeKind.Local).AddTicks(8134), "RAHULXXXX123", new DateTime(2021, 4, 9, 12, 9, 24, 490, DateTimeKind.Local).AddTicks(8207) });
        }
    }
}
