using FlowerShopAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FlowerShopAPI.Data
{
    public static class DbSeeds
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task SeedDestinations(ApplicationDbContext context)
        {
            foreach (var seed in SeedData.Destinations)
            {
                if (!context.Destinations.Any(d => d.Id == seed.Id))
                {
                    context.Destinations.Add(new Destination { Id = seed.Id, Name = seed.Name });
                }
            }

            if (context.ChangeTracker.HasChanges())
            {
                await context.SaveChangesAsync();
            }
        }
    }
}
