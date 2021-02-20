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
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    OrderStatus = table.Column<short>(type: "smallint", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PromotionId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PaymentId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    BillingAddressId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ReceipentAddressId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CustomerId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderDetail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProductPurchasePrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Tax = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    OrderId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ProductId = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SKU = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderDetail", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "BillingAddressId", "CreatedAt", "CustomerId", "InvoiceNumber", "ModifiedDate", "OrderDate", "OrderStatus", "PaymentId", "PromotionId", "ReceipentAddressId" },
                values: new object[,]
                {
                    { new Guid("d7e424f5-2f4e-483a-a86b-c2cd62d2b045"), "New Address", new DateTime(2021, 2, 20, 19, 21, 16, 679, DateTimeKind.Local).AddTicks(6792), "c2bcee97-8df0-463d-9215-2643d16da53b", "#12345", new DateTime(2021, 2, 20, 19, 21, 16, 680, DateTimeKind.Local).AddTicks(6293), new DateTime(2021, 2, 20, 19, 21, 16, 680, DateTimeKind.Local).AddTicks(6760), (short)0, "81636740-b044-47da-8588-876b66718c1b", "#1qaz2wsx", "New Address" },
                    { new Guid("914ec668-299c-47af-91a0-0dc6d7db9eb9"), "Old Address", new DateTime(2021, 2, 20, 19, 21, 16, 680, DateTimeKind.Local).AddTicks(8766), "bc696332-9ead-4af2-89d3-05cc3c2e8c20", "#4567", new DateTime(2021, 2, 20, 19, 21, 16, 680, DateTimeKind.Local).AddTicks(8810), new DateTime(2021, 2, 20, 19, 21, 16, 680, DateTimeKind.Local).AddTicks(8818), (short)1, "9f13cec1-86a1-4fa2-9f3e-60a674231b00", "#3edc$RFV", "Old Address" }
                });

            migrationBuilder.InsertData(
                table: "ProductOrderDetail",
                columns: new[] { "Id", "CreatedAt", "ModifiedDate", "OrderId", "ProductId", "ProductPurchasePrice", "Quantity", "SKU", "Tax" },
                values: new object[,]
                {
                    { new Guid("e85bec29-167c-475f-a82b-a650ff7a4747"), new DateTime(2021, 2, 20, 19, 21, 16, 682, DateTimeKind.Local).AddTicks(7706), new DateTime(2021, 2, 20, 19, 21, 16, 682, DateTimeKind.Local).AddTicks(8166), "d7e424f5-2f4e-483a-a86b-c2cd62d2b045", "1243", 0m, 2, "1235", 12m },
                    { new Guid("f02da202-224f-4c44-930e-e4e8ba621f86"), new DateTime(2021, 2, 20, 19, 21, 16, 683, DateTimeKind.Local).AddTicks(2), new DateTime(2021, 2, 20, 19, 21, 16, 683, DateTimeKind.Local).AddTicks(10), "914ec668-299c-47af-91a0-0dc6d7db9eb9", "1241", 0m, 2, "1234", 12m }
                });
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
