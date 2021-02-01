using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderService.DataAccess.SQL.Migrations
{
    public partial class OrderServiceDataAccessSQLOrderContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PromotionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderDetail",
                columns: table => new
                {
                    ProductOrderDetailID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductPurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderDetail", x => x.ProductOrderDetailID);
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "AddressId", "CustomerId", "InvoiceNumber", "ModifiedDate", "OrderDate", "OrderStatus", "PaymentId", "PromotionId" },
                values: new object[] { new Guid("3a91c648-9026-47fd-96fd-a5d95c93b6b6"), "New Address", "7121c0df-29a2-4d3f-be86-1dc9df3b39ec", "#12345", new DateTime(2021, 1, 20, 20, 55, 12, 464, DateTimeKind.Local).AddTicks(1550), new DateTime(2021, 1, 20, 20, 55, 12, 467, DateTimeKind.Local).AddTicks(2142), "Confirmed", "b98bc5ab-9e76-410b-8736-b6ef0993048a", "#1qaz2wsx" });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "OrderId", "AddressId", "CustomerId", "InvoiceNumber", "ModifiedDate", "OrderDate", "OrderStatus", "PaymentId", "PromotionId" },
                values: new object[] { new Guid("82d25416-6e9e-4463-8f1a-d94187754787"), "Old Address", "59371016-2302-4790-ba7d-6f0339343907", "#4567", new DateTime(2021, 1, 20, 20, 55, 12, 467, DateTimeKind.Local).AddTicks(5077), new DateTime(2021, 1, 20, 20, 55, 12, 467, DateTimeKind.Local).AddTicks(5103), "Awaiting", "a0baf6ec-63ee-4c88-8d6f-057ba4b456aa", "#3edc$RFV" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "ProductOrderDetail");
        }
    }
}
