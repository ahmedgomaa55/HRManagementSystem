using HRMangmentSystem.BusinessLayer.Helpers;
using HRMangmentSystem.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace HRMangmentSystem.API.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            var usersCount = _userManager.Users.Count();
            if (usersCount <= 0)
            {
                var defaultuser = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@project.com",
                    FullName = "HR Project Admin Manger",
                    PhoneNumber = "123456",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultuser, "123456Aa*");
                if (!await _roleManager.RoleExistsAsync(UserRoles.SuperAdmin))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.SuperAdmin));
                await _userManager.AddToRoleAsync(defaultuser, UserRoles.SuperAdmin);
            }
        }
    }
}
