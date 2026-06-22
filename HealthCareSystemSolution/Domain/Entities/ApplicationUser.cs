using ApplicationLayer.Entities.Enums;
using ApplicationLayer.Entities.MedicalStuffModels.DoctorModels;
using ApplicationLayer.Entities.PatientModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Gender gender { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
