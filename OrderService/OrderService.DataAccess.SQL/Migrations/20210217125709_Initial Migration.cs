using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderService.DataAccess.SQL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderStatus = table.Column<short>(type: "smallint", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PromotionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BillingAddressId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceipentAddressId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductPurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderDetail", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "BillingAddressId", "CreatedAt", "CustomerId", "InvoiceNumber", "ModifiedDate", "OrderDate", "OrderStatus", "PaymentId", "PromotionId", "ReceipentAddressId" },
                values: new object[] { new Guid("14b79f2b-fcde-402f-b4ef-a8de587d9105"), "New Address", new DateTime(2021, 2, 17, 18, 27, 9, 59, DateTimeKind.Local).AddTicks(1191), "78f7ae00-735d-4cb6-8f5a-b923c60fc09e", "#12345", new DateTime(2021, 2, 17, 18, 27, 9, 60, DateTimeKind.Local).AddTicks(1271), new DateTime(2021, 2, 17, 18, 27, 9, 60, DateTimeKind.Local).AddTicks(1781), (short)0, "e18d9d26-6314-46f1-badb-266efb7289b1", "#1qaz2wsx", "New Address" });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "BillingAddressId", "CreatedAt", "CustomerId", "InvoiceNumber", "ModifiedDate", "OrderDate", "OrderStatus", "PaymentId", "PromotionId", "ReceipentAddressId" },
                values: new object[] { new Guid("714f7f9a-82f7-47db-8c82-ae1137fcfb08"), "Old Address", new DateTime(2021, 2, 17, 18, 27, 9, 60, DateTimeKind.Local).AddTicks(4127), "ee3c6dec-298c-4d17-b53d-3e2a8c1154fe", "#4567", new DateTime(2021, 2, 17, 18, 27, 9, 60, DateTimeKind.Local).AddTicks(4179), new DateTime(2021, 2, 17, 18, 27, 9, 60, DateTimeKind.Local).AddTicks(4188), (short)1, "1565823e-5e3f-4686-9add-fcd647939940", "#3edc$RFV", "Old Address" });
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
