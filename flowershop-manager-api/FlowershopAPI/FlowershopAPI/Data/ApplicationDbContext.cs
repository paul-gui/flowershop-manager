using FlowerShopAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlowerShopAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Sale> Sales { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Price>()
                .HasIndex(p => new { p.ProductId, p.DestinationId })
                .IsUnique();

            builder.Entity<Destination>().HasData(SeedData.Destinations);
            
            builder.Entity<Sale>().Property(s => s.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            builder.Entity<Warehouse>().Property(w => w.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.IsActive).HasDefaultValue(true).ValueGeneratedOnAdd();
        }
    }
}
