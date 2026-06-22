using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities.PatientModels
{
    public class PatientAllergy : BaseEntity
    {
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int AllergyId { get; set; }
        public Allergy Allergy { get; set; }

        public decimal Severity { get; set; }
    }
}
