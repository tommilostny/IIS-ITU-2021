using Microsoft.AspNetCore.Identity;

namespace Fituska.DAL;

public static class SeedRolesAndUsers
{
    public static async Task Seed(RoleManager<IdentityRole<Guid>> roleManager, UserManager<UserEntity> userManager)
    {
        await SeedRoles(roleManager);
        await SeedAdministratorUser(userManager);
        await SeedModeratorUser(userManager);
        await SeedSampleStudentUser(userManager);
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
        if (await userManager.FindByEmailAsync("admin@fituska.net") is null)
        {
            var adminUser = new UserEntity
            {
                UserName = "administrator",
                Email = "admin@fituska.net",
                RegistrationDate = DateTime.UtcNow,
            };
            var identityResult = await userManager.CreateAsync(adminUser, "Password1!");

            if (identityResult.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, RoleNames.AdminRoleName);
            }
        }
    }

    private static async Task SeedModeratorUser(UserManager<UserEntity> userManager)
    {
        if (await userManager.FindByEmailAsync("moderator1@fituska.net") is null)
        {
            var modUser = new UserEntity
            {
                UserName = "moderator",
                Email = "moderator1@fituska.net",
                RegistrationDate = DateTime.UtcNow,
            };
            var identityResult = await userManager.CreateAsync(modUser, "Password1");

            if (identityResult.Succeeded)
            {
                await userManager.AddToRoleAsync(modUser, RoleNames.AdminRoleName);
            }
        }
    }

    private static async Task SeedSampleStudentUser(UserManager<UserEntity> userManager)
    {
        if (await userManager.FindByEmailAsync("xfranta01@stud.fit.vutbr.cz") is null)
        {
            var modUser = new UserEntity
            {
                UserName = "franta",
                Email = "xfranta01@stud.fit.vutbr.cz",
                RegistrationDate = DateTime.UtcNow,
            };
            var identityResult = await userManager.CreateAsync(modUser, "franta1");

            if (identityResult.Succeeded)
            {
                await userManager.AddToRoleAsync(modUser, RoleNames.AdminRoleName);
            }
        }
    }
}
