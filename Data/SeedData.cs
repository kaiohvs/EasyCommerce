using EasyCommerce.Autentication;
using Microsoft.AspNetCore.Identity;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var roleName = "Admin";
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        var adminUser = new ApplicationUser
        {
            UserName = "admin@admin.com",
            Email = "admin@admin.com",
            EmailConfirmed = true // Marcar como confirmado para evitar verificação de email
        };


        if (userManager.Users.All(u => u.Id != adminUser.Id))
        {
            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var createAdmin = await userManager.CreateAsync(adminUser, "Admin@123456");
                if (createAdmin.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, roleName);

                }
            }
        }
    }
}
