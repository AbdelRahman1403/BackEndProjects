using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Patient
{
    public class PatientAllergies : BaseEntity<int>
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
        public int AllergyId { get; set; }
        public Allergies Allergy { get; set; } = null!;
    }
}
