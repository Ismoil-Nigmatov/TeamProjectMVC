using Microsoft.AspNetCore.Identity;
using TeamProjectMVC.Entity.Enums;
using TeamProjectMVC.Entity;
using TeamProjectMVC.Repository;

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
                if (!await roleManager.RoleExistsAsync((ERole.USER).ToString()))
                    await roleManager.CreateAsync(new IdentityRole((ERole.USER).ToString()));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin1@gmail.com";
                string adminUserEmail2 = "admin2@gmail.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "Admin_1",
                        Email = adminUserEmail,
                        EmailConfirmed = true,

                    };
                    var newAdminUser2 = new User()
                    {
                        UserName = "Admin_2",
                        Email = adminUserEmail2,
                        EmailConfirmed = true,

                    };
                    await userManager.CreateAsync(newAdminUser, "Admin1@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, (ERole.ADMIN).ToString());
                    //   2

                    await userManager.CreateAsync(newAdminUser2, "Admin2@1234?");
                    await userManager.AddToRoleAsync(newAdminUser2, (ERole.ADMIN).ToString());


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

                var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
                if (!dbContext.Products.Any())
                {
                    var productRepository = serviceScope.ServiceProvider.GetRequiredService<IProductRepository>();
                    var defaultProducts = new List<Product>
                    {
                        new Product { Name = "HDD 1TB", Quantity = 55, Price = 74.09, ToTalPrice = await productRepository.CalculateTotalPrice(55, 74.09) },
                        new Product { Name = "HDD SSD 512GB", Quantity = 102, Price = 190.99, ToTalPrice = await productRepository.CalculateTotalPrice(102, 190.99) },
                        new Product { Name = "RAM DDR4 16GB", Quantity = 47, Price = 80.32, ToTalPrice = await productRepository.CalculateTotalPrice(47, 80.32) }
                    };
                    dbContext.Products.AddRange(defaultProducts);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
