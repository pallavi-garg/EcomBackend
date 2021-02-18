using Microsoft.EntityFrameworkCore;
using System;

namespace CartService.DataAccess.SQL
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions opts): base(opts)
        {

        }

        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartProductMapping> CartProductMapping { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasData(new Cart
            {
                CreatedAt = DateTime.Now,
                CustomerId = "RAHULXXXX123",
                Id = Guid.NewGuid(),
                ModifiedDate = DateTime.Now

            }, new Cart
            {
                CreatedAt = DateTime.Now.AddDays(50),
                CustomerId = "RAHULXXXX123",
                Id = Guid.NewGuid(),
                ModifiedDate = DateTime.Now.AddDays(50)

            }) ;

            modelBuilder.Entity<CartProductMapping>().HasData(new CartProductMapping
            {
                CartId = "8FA73BC7-FE97-4C67-9AC3-2991958F6469",
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Quantity = 2,
                ProductId = "XYZ_12345",
                SKU = "ADCSKKK_SSW#$%&**SS^&*()"
               
            });
            modelBuilder.Entity<CartProductMapping>().HasData(new CartProductMapping
            {
                CartId = "325C5EE8-BA74-47C5-9E4C-A8BD24A9B570",
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Quantity = 2,
                ProductId = "ABCD_XXXX",
                SKU = "ADCSKKK_SSWqweeqweqweqwe"

            });

        }
    }
}
