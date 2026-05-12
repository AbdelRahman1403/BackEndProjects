using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Patient
{
    public class PatientPhoneNumbers : BaseEntity<int>
    {
        public string PhoneNumber { get; set; } = null!;
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}
