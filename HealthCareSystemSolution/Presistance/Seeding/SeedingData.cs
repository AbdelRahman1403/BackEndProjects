using ApplicationLayer.Entities;
using ApplicationLayer.Entities.MedicalStuffModels.DoctorModels;
using ApplicationLayer.Seeding;
using Microsoft.AspNetCore.Identity;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeding
{
    public class SeedingData(HealthCareDbContext dbContext, RoleManager<IdentityRole<int>> role , UserManager<ApplicationUser> userManager) : ISeedingData
    {
        public async Task SeedDataAsync()
        {
            if (!role.Roles.Any())
            {
                await role.CreateAsync(new IdentityRole<int> { Name = "Admin" });
                await role.CreateAsync(new IdentityRole<int> { Name = "Doctor" });
                await role.CreateAsync(new IdentityRole<int> { Name = "Patient" });
            }

            if (!dbContext.MedicalSpecialties.Any())
            {
                await dbContext.MedicalSpecialties.AddRangeAsync(new List<MedicalSpecialty>
                {
                    new MedicalSpecialty { Major = "Cardiology" },
                    new MedicalSpecialty { Major = "Dermatology" },
                    new MedicalSpecialty { Major = "Neurology" },
                    new MedicalSpecialty { Major = "Pediatrics" },
                    new MedicalSpecialty { Major = "Psychiatry" }
                });
            }

            if (!userManager.Users.Any())
            {
                var user01 = new ApplicationUser()
                {
                    UserName = "SystemAdmin",
                    Email = "SystemTest@gmail.com",
                    FirstName = "HealthCare",
                    LastName = "System"
                };
                await userManager.AddToRoleAsync(user01, "Admin");
                await userManager.CreateAsync(user01 , "P@ssw0rd");
            }
            await dbContext.SaveChangesAsync();
        }
    }
}
