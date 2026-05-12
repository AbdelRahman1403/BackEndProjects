using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.MedicalStuff.Doctor
{
    public class MedicalSpecialty : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public ICollection<Doctor> Doctors { get; set; }
    }
}
