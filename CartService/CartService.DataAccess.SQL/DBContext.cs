using Microsoft.EntityFrameworkCore;
using System;

namespace CartService.DataAccess.SQL
{
    public class DBContext: DbContext
    {
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartProductMapping> CartProductMapping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { optionsBuilder.UseSqlServer("server=localhost;database=CartServiceDB;Integrated Security=SSPI;MultipleActiveResultSets=True;"); }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartProductMapping>()
                .HasKey(c => new { c.CartId, c.ProductId });
        }
    }
}
