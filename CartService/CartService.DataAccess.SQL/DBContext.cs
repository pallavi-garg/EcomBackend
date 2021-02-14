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

        }
    }
}
