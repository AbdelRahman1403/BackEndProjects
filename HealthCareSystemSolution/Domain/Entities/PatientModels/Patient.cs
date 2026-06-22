using ApplicationLayer.Entities.AppointmentsModels;
using ApplicationLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities.PatientModels
{
    public class Patient : BaseEntity
    {
        public int applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; } = null!;
        public BloodType bloodType { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public ICollection<PhoneNumbers> PhoneNumbers { get; set; }
        public ICollection<PatientAllergy> PatientAllergy { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

    }
}
