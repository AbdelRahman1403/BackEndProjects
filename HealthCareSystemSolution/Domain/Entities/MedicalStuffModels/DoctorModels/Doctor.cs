using ApplicationLayer.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities.MedicalStuffModels.DoctorModels
{
    public class Doctor : BaseEntity
    {
        public int applicationUserId { get; set; }
        public ApplicationUser applicationUser { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Evaluation { get; set; }
        public ICollection<ReservationTime> ReservationTimes { get; set; } = null!;
        public ICollection<MedicalPhoneNumbers> MedicalPhoneNumbers { get; set; } = null!;
        public MedicalSpecialty MedicalSpecialty { get; set; }
         public int MedicalSpecialtyId { get; set; }
        public MedicalDegreeLevel MedicalDegreeLevel { get; set; }
    }
}
