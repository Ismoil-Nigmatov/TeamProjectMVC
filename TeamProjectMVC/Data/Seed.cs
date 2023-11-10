using Microsoft.AspNetCore.Identity;
using TeamProjectMVC.Entity.Enums;
using TeamProjectMVC.Entity;

namespace TeamProjectMVC.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync((ERole.ADMIN).ToString()))
                    await roleManager.CreateAsync(new IdentityRole((ERole.ADMIN).ToString()));
                if (!await roleManager.RoleExistsAsync((ERole.USER).ToSt ring()))
                    await roleManager.CreateAsync(new IdentityRole((ERole.USER).ToString()));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "app-admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, (ERole.ADMIN).ToString());
                }

                string appUserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName ="app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, (ERole.USER).ToString());
                }
            }
        }



    }
}
