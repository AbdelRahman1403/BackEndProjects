using ApplicationLayer.Entities.MedicalStuffModels.DoctorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities.MedicalStuffModels
{
    public class MedicalPhoneNumbers : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
