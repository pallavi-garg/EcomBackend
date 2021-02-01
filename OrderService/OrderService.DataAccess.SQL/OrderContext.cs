using Microsoft.EntityFrameworkCore;
using OrderService.Shared.Model;
using System;

namespace OrderService.DataAccess.SQL
{
    public class OrderContext: DbContext
    {
        public OrderContext(DbContextOptions opts): base(opts)
        {

        }

        public DbSet<OrderDetails> Order { get; set; }
        public DbSet<ProductOrderDetail> ProductOrderDetail { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>().HasData(new OrderDetails
            {
                AddressId = "New Address",
                CustomerId = Guid.NewGuid().ToString(),
                InvoiceNumber = "#12345",
                ModifiedDate = DateTime.Now,
                OrderDate = DateTime.Now,
                OrderId = Guid.NewGuid(),
                OrderStatus = "Confirmed",
                PaymentId = Guid.NewGuid().ToString(),
                PromotionId = "#1qaz2wsx"

            }, new OrderDetails
            {
                AddressId = "Old Address",
                CustomerId = Guid.NewGuid().ToString(),
                InvoiceNumber = "#4567",
                ModifiedDate = DateTime.Now,
                OrderDate = DateTime.Now,
                OrderId = Guid.NewGuid(),
                OrderStatus = "Awaiting",
                PaymentId = Guid.NewGuid().ToString(),
                PromotionId = "#3edc$RFV"
            });

        }
    }
}
