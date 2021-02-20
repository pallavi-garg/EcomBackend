using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace CartService.DataAccess.SQL
{
    public class DBContext: DbContext
    {
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartProductMapping> CartProductMapping { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) { optionsBuilder.UseMySql("server=localhost;port=3306;database=CartServiceDB;user=root;Password=Password@123", new MySqlServerVersion(new System.Version(8, 0, 21))); };
       
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartProductMapping>()
                .HasKey(c => new { c.CartId, c.ProductId });
        }
    }
}
