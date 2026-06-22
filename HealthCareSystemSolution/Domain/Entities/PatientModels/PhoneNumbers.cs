using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities.PatientModels
{
    public class PhoneNumbers : BaseEntity
    {
        public string Number { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
