using FlowershopAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlowershopAPI.Data
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
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Price>()
                .HasIndex(p => new { p.ProductId, p.DestinationId })
                .IsUnique();

            builder.Entity<Destination>().HasData(
                new Destination {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Florărie",
                },
                new Destination {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "En Gros",
                }
            );
        }
    }
}
