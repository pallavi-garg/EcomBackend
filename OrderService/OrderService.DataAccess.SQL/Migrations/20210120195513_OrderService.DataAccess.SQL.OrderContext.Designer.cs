﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderService.DataAccess.SQL;

namespace OrderService.DataAccess.SQL.Migrations
{
    [DbContext(typeof(OrderContext))]
    [Migration("20210120195513_OrderService.DataAccess.SQL.OrderContext")]
    partial class OrderServiceDataAccessSQLOrderContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("OrderService.Shared.Model.OrderDetails", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AddressId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PromotionId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.ToTable("Order");

                    b.HasData(
                        new
                        {
                            OrderId = new Guid("3a91c648-9026-47fd-96fd-a5d95c93b6b6"),
                            AddressId = "New Address",
                            CustomerId = "7121c0df-29a2-4d3f-be86-1dc9df3b39ec",
                            InvoiceNumber = "#12345",
                            ModifiedDate = new DateTime(2021, 1, 20, 20, 55, 12, 464, DateTimeKind.Local).AddTicks(1550),
                            OrderDate = new DateTime(2021, 1, 20, 20, 55, 12, 467, DateTimeKind.Local).AddTicks(2142),
                            OrderStatus = "Confirmed",
                            PaymentId = "b98bc5ab-9e76-410b-8736-b6ef0993048a",
                            PromotionId = "#1qaz2wsx"
                        },
                        new
                        {
                            OrderId = new Guid("82d25416-6e9e-4463-8f1a-d94187754787"),
                            AddressId = "Old Address",
                            CustomerId = "59371016-2302-4790-ba7d-6f0339343907",
                            InvoiceNumber = "#4567",
                            ModifiedDate = new DateTime(2021, 1, 20, 20, 55, 12, 467, DateTimeKind.Local).AddTicks(5077),
                            OrderDate = new DateTime(2021, 1, 20, 20, 55, 12, 467, DateTimeKind.Local).AddTicks(5103),
                            OrderStatus = "Awaiting",
                            PaymentId = "a0baf6ec-63ee-4c88-8d6f-057ba4b456aa",
                            PromotionId = "#3edc$RFV"
                        });
                });

            modelBuilder.Entity("OrderService.Shared.Model.ProductOrderDetail", b =>
                {
                    b.Property<Guid>("ProductOrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProductPurchasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SKU")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Tax")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductOrderDetailID");

                    b.ToTable("ProductOrderDetail");
                });
#pragma warning restore 612, 618
        }
    }
}
