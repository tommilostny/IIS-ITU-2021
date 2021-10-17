using Microsoft.AspNetCore.Identity;

namespace Fituska.Server.Data;

internal static class SeedRolesAndUsers
{
    internal static async Task Seed(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
        await SeedAdministratorRole(roleManager);
        await SeedAdministratorUser(userManager);
    }

    private static async Task SeedAdministratorRole(RoleManager<IdentityRole> roleManager)
    {
        bool adminRoleExists = await roleManager.RoleExistsAsync("Administrator");

        if (!adminRoleExists)
        {
            var adminRole = new IdentityRole
            {
                Name = "Administrator"
            };
            await roleManager.CreateAsync(adminRole);
        }
    }

    private static async Task SeedAdministratorUser(UserManager<IdentityUser> userManager)
    {
        bool adminUserExists = await userManager.FindByEmailAsync("admin@fituska.net") is not null;

        if  (!adminUserExists)
        {
            var adminUser = new IdentityUser
            {
                UserName = "admin@fituska.net",
                Email = "admin@fituska.net"
            };
            var identityResult = await userManager.CreateAsync(adminUser, "Password1!");

            if (identityResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Administrator");
            }
        }
    }
}
