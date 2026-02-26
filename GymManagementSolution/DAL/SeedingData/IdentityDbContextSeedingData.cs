using DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.SeedingData
{
    public class IdentityDbContextSeedingData
    {
        public static void SeedData(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            // 1. Check independently (Don't use &&, or one existing will block the other from seeding)
            bool rolesExist = roleManager.Roles.Any();
            bool usersExist = userManager.Users.Any();

            // 2. Seed Roles
            if (!rolesExist)
            {
                var roles = new List<IdentityRole>
        {
            new() { Name = "SuperAdmin" },
            new() { Name = "Admin" },
        };

                foreach (var role in roles)
                {
                    // Use GetAwaiter().GetResult() to block the thread until finished
                    if (!roleManager.RoleExistsAsync(role.Name).GetAwaiter().GetResult())
                    {
                        roleManager.CreateAsync(role).GetAwaiter().GetResult();
                    }
                }
            }

            // 3. Seed Users
            if (!usersExist)
            {
                var mainAdmin = new ApplicationUser()
                {
                    FirstName = "Abdo",
                    LastName = "Yassin",
                    UserName = "abdoyassin",
                    Email = "yassin@gmail.com",
                    PhoneNumber = "01066619400",
                    EmailConfirmed = true // Highly recommended
                };

                // CRITICAL: You must block the thread here
                var result = userManager.CreateAsync(mainAdmin, "P1@ssw@ord").GetAwaiter().GetResult();

                if (result.Succeeded)
                {
                    // Now that the user definitely exists, assign the role
                    userManager.AddToRoleAsync(mainAdmin, "SuperAdmin").GetAwaiter().GetResult();
                }

                var adminUser = new ApplicationUser()
                {
                    FirstName = "basel",
                    LastName = "hegazi",
                    UserName = "baselhegazi",
                    Email = "baselhegazi@gmail.com",
                    PhoneNumber = "01178619460",
                    EmailConfirmed = true
                };

                var resultAdmin = userManager.CreateAsync(adminUser, "P2@ssw@ord").GetAwaiter().GetResult();

                if (resultAdmin.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                }
            }
        }
    }
}
