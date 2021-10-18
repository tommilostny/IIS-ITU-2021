using Fituska.Server.Entities;
using Fituska.Shared.Static;
using Microsoft.AspNetCore.Identity;

namespace Fituska.Server.Data;

internal static class SeedRolesAndUsers
{
    internal static async Task Seed(RoleManager<IdentityRole<Guid>> roleManager, UserManager<UserEntity> userManager)
    {
        await SeedRoles(roleManager);
        await SeedAdministratorUser(userManager);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
    {
        foreach (var roleName in RoleNames.GetAll())
        {
            if (await roleManager.RoleExistsAsync(roleName))
                continue;

            var role = new IdentityRole<Guid>
            {
                Name = roleName
            };
            await roleManager.CreateAsync(role);
        }
    }

    private static async Task SeedAdministratorUser(UserManager<UserEntity> userManager)
    {
        bool adminUserExists = await userManager.FindByEmailAsync("admin@fituska.net") is not null;

        if  (!adminUserExists)
        {
            var adminUser = new UserEntity
            {
                UserName = "admin@fituska.net",
                Email = "admin@fituska.net"
            };
            var identityResult = await userManager.CreateAsync(adminUser, "Password1!");

            if (identityResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, RoleNames._adminRoleName);
            }
        }
    }
}
