using Microsoft.EntityFrameworkCore;
using OrderService.Shared.Model;
using System;

namespace OrderService.DataAccess.SQL
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions opts): base(opts)
        {

        }

        public DbSet<OrderDetails> Order { get; set; }
        public DbSet<ProductOrderDetail> ProductOrderDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var order1 = new OrderDetails
            {
                BillingAddressId = "New Address",
                ReceipentAddressId = "New Address",
                CustomerId = Guid.NewGuid().ToString(),
                InvoiceNumber = "#12345",
                ModifiedDate = DateTime.Now,
                OrderDate = DateTime.Now,
                Id = Guid.NewGuid(),
                OrderStatus = 0,
                PaymentId = Guid.NewGuid().ToString(),
                PromotionId = "#1qaz2wsx"

            };

            var order2 = new OrderDetails
            {
                BillingAddressId = "Old Address",
                ReceipentAddressId = "Old Address",
                CustomerId = Guid.NewGuid().ToString(),
                InvoiceNumber = "#4567",
                ModifiedDate = DateTime.Now,
                OrderDate = DateTime.Now,
                Id = Guid.NewGuid(),
                OrderStatus = 1,
                PaymentId = Guid.NewGuid().ToString(),
                PromotionId = "#3edc$RFV"
            };

            modelBuilder.Entity<OrderDetails>().HasData(order1, order2);

            modelBuilder.Entity<ProductOrderDetail>().HasData(new ProductOrderDetail
            {
                OrderId = order1.Id.ToString(),
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Quantity = 2,
                ProductId = "1243",
                SKU = "1235",
                Tax = 12

            });
            modelBuilder.Entity<ProductOrderDetail>().HasData(new ProductOrderDetail
            {
                OrderId = order2.Id.ToString(),
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Quantity = 2,
                ProductId = "1241",
                SKU = "1234",
                Tax = 12

            });

        }
    }
}
