using GoSportsAPI.Data;
using GoSportsAPI.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace GoSportsAPI
{
    public class IdentitySeed
    {
        private static readonly string[] Roles = { "Admin", "User" };

        private static readonly (string Name, string Email, string Password, string Role)[] DefaultUsers = new[]
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

                    var created = await userManager.CreateAsync(user, password);
                    if (!created.Succeeded)
                        throw new Exception($"Failed to create user {email}: {string.Join(", ", created.Errors.Select(e => e.Description))}");
                }

                if (!await userManager.IsInRoleAsync(user, role))
                {
                    await userManager.AddToRoleAsync(user, role);
                }

                var hasProfile = await db.Set<UserProfile>().AnyAsync(p => p.Id == user.Id);
                if (!hasProfile)
                {
                    db.Set<UserProfile>().Add(new UserProfile { Id = user.Id, User = user });
                }
            }

            await db.SaveChangesAsync();
        }
    }
}