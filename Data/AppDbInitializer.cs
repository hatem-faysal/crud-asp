using crud2.Data.Static;
using crud2.Models;
using Microsoft.AspNetCore.Identity;

namespace crud2.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder builder)
        {
            using (var applicationservices = builder.ApplicationServices.CreateScope())
            {
                var context = applicationservices.ServiceProvider.GetService<EcommerceDbContext>();
                context.Database.EnsureCreated();
                if (!context.Categories.Any())
                {
                    var caategoreies = new List<Category>()
                    {
                        new Category()
                        {
                            Name="c1",
                        },
                        new Category()
                        {
                            Name="c2",
                        },
                        new Category()
                        {
                            Name="c3",
                        },
                    };
                    context.Categories.AddRange(caategoreies);
                    context.SaveChanges();
                }

            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder builder)
        {
            using(var applicationservices = builder.ApplicationServices.CreateScope())
            {
                #region Role
                var roleManager =
                    applicationservices.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }
                #endregion

                #region User
                var userManager =
                    applicationservices.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                if (await userManager.FindByEmailAsync("admin@admin.com") == null)
                {
                    var NewAdminUser = new ApplicationUser()
                    {
                        Email = "admin@admin.com",
                        EmailConfirmed = true,
                        FullName = "Admin User",
                        UserName = "Admin",
                    };
                    await userManager.CreateAsync(NewAdminUser, "@Dmin123");
                    await userManager.AddToRoleAsync(NewAdminUser, UserRoles.Admin);
                }
                if (await userManager.FindByEmailAsync("user@user.com") == null)
                {
                    var newOridinalUser = new ApplicationUser()
                    {
                        Email = "user@user.com",
                        EmailConfirmed = true,
                        FullName = "Oridinal User",
                        UserName = "User",
                    };
                    await userManager.CreateAsync(newOridinalUser, "@User123");
                    await userManager.AddToRoleAsync(newOridinalUser, UserRoles.User);
                }
                #endregion
            }
        }
    }
}
