using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderService.DataAccess.SQL.Migrations
{
    public partial class UpdateFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: new Guid("3a91c648-9026-47fd-96fd-a5d95c93b6b6"));

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "OrderId",
                keyValue: new Guid("82d25416-6e9e-4463-8f1a-d94187754787"));

            migrationBuilder.RenameColumn(
                name: "ProductOrderDetailID",
                table: "ProductOrderDetail",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Order",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ProductOrderDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ProductOrderDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Order",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "AddressId", "CreatedAt", "CustomerId", "InvoiceNumber", "ModifiedDate", "OrderDate", "OrderStatus", "PaymentId", "PromotionId" },
                values: new object[] { new Guid("99a673d1-957a-435c-8ce5-b5cff95f8732"), "New Address", new DateTime(2021, 2, 14, 15, 22, 8, 500, DateTimeKind.Local).AddTicks(435), "a343e671-93af-47cb-b7a8-99fac10f1f01", "#12345", new DateTime(2021, 2, 14, 15, 22, 8, 503, DateTimeKind.Local).AddTicks(2196), new DateTime(2021, 2, 14, 15, 22, 8, 503, DateTimeKind.Local).AddTicks(2754), "Confirmed", "33bafcb3-ccc7-435d-98fc-49f4b13cd71a", "#1qaz2wsx" });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "AddressId", "CreatedAt", "CustomerId", "InvoiceNumber", "ModifiedDate", "OrderDate", "OrderStatus", "PaymentId", "PromotionId" },
                values: new object[] { new Guid("5fac4791-15bb-453a-aaa9-5ed35f62f1de"), "Old Address", new DateTime(2021, 2, 14, 15, 22, 8, 503, DateTimeKind.Local).AddTicks(5270), "54ca7ed4-0042-4a0f-97ff-c0f9fad12c43", "#4567", new DateTime(2021, 2, 14, 15, 22, 8, 503, DateTimeKind.Local).AddTicks(5321), new DateTime(2021, 2, 14, 15, 22, 8, 503, DateTimeKind.Local).AddTicks(5330), "Awaiting", "e5c70f5f-3cdd-43ed-a1cc-d009fe7e0166", "#3edc$RFV" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("5fac4791-15bb-453a-aaa9-5ed35f62f1de"));

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: new Guid("99a673d1-957a-435c-8ce5-b5cff95f8732"));

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ProductOrderDetail");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ProductOrderDetail");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Order");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductOrderDetail",
                newName: "ProductOrderDetailID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Order",
                newName: "OrderId");
        }
    }
}
