using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities.MedicalStuffModels.DoctorModels
{
    public class MedicalSpecialty : BaseEntity
    {
        public string Major { get; set; } = null!;
        public ICollection<Doctor> Doctors { get; set; }
    }
}
