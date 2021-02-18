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
            Cart cart1 = new Cart
            {
                CreatedAt = DateTime.Now,
                CustomerId = "RAHULXXXX123",
                Id = Guid.NewGuid(),
                ModifiedDate = DateTime.Now,
                CartId = "8FA73BC7-FE97-4C67-9AC3-2991958F6469"
            };
            Cart cart2 = new Cart
            {
                CreatedAt = DateTime.Now.AddDays(50),
                CustomerId = "RAJXXXX123",
                Id = Guid.NewGuid(),
                ModifiedDate = DateTime.Now.AddDays(50),
                CartId = "325C5EE8-BA74-47C5-9E4C-A8BD24A9B570"
            };
            modelBuilder.Entity<Cart>().HasData(cart1, cart2);

            modelBuilder.Entity<CartProductMapping>().HasData(new CartProductMapping
            {
                CartId = cart1.CartId,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Quantity = 2,
                ProductId = "1243",
                SKU = "1235"

            });
            modelBuilder.Entity<CartProductMapping>().HasData(new CartProductMapping
            {
                CartId = cart2.CartId,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Quantity = 2,
                ProductId = "1241",
                SKU = "1234"

            });

        }
    }
}
