using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities.PatientModels
{
    public class Allergy : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<PatientAllergy> PatientAllergies { get; set; } = new List<PatientAllergy>();
    }
}
