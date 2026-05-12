using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.MedicalStuff.Doctor
{
    public class Doctor : BaseEntity<int>
    {
        public string FullName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public decimal Evaluation { get; set; }
        public int MedicalSpecialtyId { get; set; }
        public MedicalSpecialty MedicalSpecialty { get; set; } = null!;
        public ICollection<ReservationTime> ReservationTimes { get; set; } = null!;
        public MedicalDegreePhase MedicalDegreePhase { get; set; }


    }
}
