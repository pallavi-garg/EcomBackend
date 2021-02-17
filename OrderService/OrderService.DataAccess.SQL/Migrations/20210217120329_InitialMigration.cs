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
                values: new object[] { new Guid("4d4b8bcc-66d1-42b2-a0fa-dde8ba2c9d8e"), "New Address", new DateTime(2021, 2, 17, 17, 33, 29, 376, DateTimeKind.Local).AddTicks(2297), "f9d7cd3b-bd1d-4b0c-9eda-081b59335c97", "#12345", new DateTime(2021, 2, 17, 17, 33, 29, 377, DateTimeKind.Local).AddTicks(2359), new DateTime(2021, 2, 17, 17, 33, 29, 377, DateTimeKind.Local).AddTicks(2856), (short)0, "eb3a097c-d677-4f76-9dcc-265e7cf7baaa", "#1qaz2wsx", "New Address" });

            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "BillingAddressId", "CreatedAt", "CustomerId", "InvoiceNumber", "ModifiedDate", "OrderDate", "OrderStatus", "PaymentId", "PromotionId", "ReceipentAddressId" },
                values: new object[] { new Guid("7668eea2-23b7-481b-bae4-b35b62b8cab2"), "Old Address", new DateTime(2021, 2, 17, 17, 33, 29, 377, DateTimeKind.Local).AddTicks(5130), "36192d7f-0987-4db5-a47f-240bfb80f5de", "#4567", new DateTime(2021, 2, 17, 17, 33, 29, 377, DateTimeKind.Local).AddTicks(5182), new DateTime(2021, 2, 17, 17, 33, 29, 377, DateTimeKind.Local).AddTicks(5192), (short)1, "9a9367ab-db44-4953-800e-fea964f2f52f", "#3edc$RFV", "Old Address" });
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
