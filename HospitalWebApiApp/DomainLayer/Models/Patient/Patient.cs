using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Patient
{
    public class Patient : BaseEntity<int>
    {
        public string FullName { get; set; } = null!;
        public DateOnly DOB { get; set; }
        public string Description { get; set; } = null!;
        public Gender Gender { get; set; }
        public BloodType BloodType { get; set; }

        public ICollection<PatientPhoneNumbers> PatientPhoneNumbers { get; set; } = null!;

    }
}
