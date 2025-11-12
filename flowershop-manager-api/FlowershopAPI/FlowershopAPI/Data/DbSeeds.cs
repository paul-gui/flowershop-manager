using FlowershopAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FlowershopAPI.Data
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

        public static async Task SeedAdminUser(UserManager<User> userManager)
        {
            string adminEmail = "***REMOVED***";
            string adminPassword = "***REMOVED***"; // Use environment variable in production
            string adminName = "Admin";

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                var user = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = adminName
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        public static async Task SeedDestinations(ApplicationDbContext context)
        {
            if (!context.Destinations.Any())
            {
                context.Destinations.AddRange(
                    new Destination { Id =Guid.NewGuid(), Name = "Florarie"},
                    new Destination { Id = Guid.NewGuid(), Name = "En Gros"}
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
