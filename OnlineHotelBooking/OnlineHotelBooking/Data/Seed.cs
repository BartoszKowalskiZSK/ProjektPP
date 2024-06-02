using Microsoft.AspNetCore.Identity;
using OnlineHotelBooking.Data;
using OnlineHotelBooking.Models;
using OnlineHotelBooking.Data.Enum;
using OnlineHotelBooking.Models;
using System.Diagnostics;
using System.Net;

namespace RunGroopWebApp.Data
{
    public class Seed
    {

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Worker))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Worker));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "admin@example.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "admin",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Name = "admin"
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@example.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new User()
                    {
                        UserName = "user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Name = "user"
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }

                string appWorkerEmail = "worker@example.com";

                var appWorker = await userManager.FindByEmailAsync(appWorkerEmail);
                if (appWorker == null)
                {
                    var newAppWoker = new User()
                    {
                        UserName = "worker",
                        Email = appWorkerEmail,
                        EmailConfirmed = true,
                        Name = "worker"
                    };
                    await userManager.CreateAsync(newAppWoker, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppWoker, UserRoles.Worker);
                }
            }
        }
    }
}