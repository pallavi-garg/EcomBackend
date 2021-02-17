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
            modelBuilder.Entity<OrderDetails>().HasData(new OrderDetails
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

            }, new OrderDetails
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
            });

        }
    }
}
