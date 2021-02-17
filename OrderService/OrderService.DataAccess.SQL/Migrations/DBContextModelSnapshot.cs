﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderService.DataAccess.SQL;

namespace OrderService.DataAccess.SQL.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("OrderService.Shared.Model.OrderDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BillingAddressId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<short>("OrderStatus")
                        .HasColumnType("smallint");

                    b.Property<string>("PaymentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PromotionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceipentAddressId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            Id = new Guid("14b79f2b-fcde-402f-b4ef-a8de587d9105"),
                            BillingAddressId = "New Address",
                            CreatedAt = new DateTime(2021, 2, 17, 18, 27, 9, 59, DateTimeKind.Local).AddTicks(1191),
                            CustomerId = "78f7ae00-735d-4cb6-8f5a-b923c60fc09e",
                            InvoiceNumber = "#12345",
                            ModifiedDate = new DateTime(2021, 2, 17, 18, 27, 9, 60, DateTimeKind.Local).AddTicks(1271),
                            OrderDate = new DateTime(2021, 2, 17, 18, 27, 9, 60, DateTimeKind.Local).AddTicks(1781),
                            OrderStatus = (short)0,
                            PaymentId = "e18d9d26-6314-46f1-badb-266efb7289b1",
                            PromotionId = "#1qaz2wsx",
                            ReceipentAddressId = "New Address"
                        },
                        new
                        {
                            Id = new Guid("714f7f9a-82f7-47db-8c82-ae1137fcfb08"),
                            BillingAddressId = "Old Address",
                            CreatedAt = new DateTime(2021, 2, 17, 18, 27, 9, 60, DateTimeKind.Local).AddTicks(4127),
                            CustomerId = "ee3c6dec-298c-4d17-b53d-3e2a8c1154fe",
                            InvoiceNumber = "#4567",
                            ModifiedDate = new DateTime(2021, 2, 17, 18, 27, 9, 60, DateTimeKind.Local).AddTicks(4179),
                            OrderDate = new DateTime(2021, 2, 17, 18, 27, 9, 60, DateTimeKind.Local).AddTicks(4188),
                            OrderStatus = (short)1,
                            PaymentId = "1565823e-5e3f-4686-9add-fcd647939940",
                            PromotionId = "#3edc$RFV",
                            ReceipentAddressId = "Old Address"
                        });
                });

            modelBuilder.Entity("OrderService.Shared.Model.ProductOrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("ProductOrderDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
