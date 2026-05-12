using Core.DomainLayer.Models.IdentityModels;
using DomainLayer.Seeding;
using Microsoft.AspNetCore.Identity;
using Perisistence.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perisistence.SeedData
{
    public class SeedingIdentityData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, StoreIdentityDbContext dbContext) : ISeedingIdentityData
    {
        public async void DataSeedAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
                    await roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
                }
                if (!userManager.Users.Any())
                {
                    var user1 = new ApplicationUser()
                    {
                        Email = "aboyassin03@gmail.com",
                        DisplayName = "Abdelrahman Yassin",
                        PhoneNumber = "01099562215",
                        UserName = "abdoyassin"
                    };
                    var user2 = new ApplicationUser()
                    {
                        Email = "baselhegazi03@gmail.com",
                        DisplayName = "Basel Hegazi",
                        PhoneNumber = "01065609106",
                        UserName = "bsbshegazi"
                    };

                    await userManager.CreateAsync(user1, "P@ssw0rd");
                    await userManager.CreateAsync(user2, "P@ssw0rd");

                    await userManager.AddToRoleAsync(user1, "Admin");
                    await userManager.AddToRoleAsync(user2, "SuperAdmin");

                    await dbContext.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}