using GoSportsAPI.Data;
using GoSportsAPI.Models.Sports;
using GoSportsAPI.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace GoSportsAPI
{
    public class IdentitySeed
    {
        private static readonly string[] Roles = { "Admin", "User" };

        private static readonly (string Name, string Email, string Password, string Role)[] DefaultUsers =
        {
            ("Admin User",  "admin@site.com", "A$m1n1st@t0r", "Admin"),
            ("Regular User","user@site.com",  "P@ssw0rd_123",  "User")
        };

        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            foreach (var (name, email, password, role) in DefaultUsers)
            {
                var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    user = new AppUser
                    {
                        Id = Guid.NewGuid(),
                        UserName = email,
                        Email = email,
                        EmailConfirmed = true
                    };

                    var result = await userManager.CreateAsync(user, password);
                    if (!result.Succeeded)
                    {
                        throw new Exception(
                            $"Failed to create user {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}"
                        );
                    }
                }

                // Assign role if not already assigned
                if (!await userManager.IsInRoleAsync(user, role))
                    await userManager.AddToRoleAsync(user, role);

                //  Create profile if missing
                var hasProfile = await db.UserProfiles.AnyAsync(p => p.UserId == user.Id);
                if (!hasProfile)
                {
                    db.UserProfiles.Add(new UserProfile
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        LobbyId = null,
                        Sports = new List<Sport>()
                    });
                }
            }

            await db.SaveChangesAsync();
        }
    }

}