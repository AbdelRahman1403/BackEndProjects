using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.MedicalStuff.Doctor
{
    public class ReservationTime : BaseEntity<int>
    {
        public DateTime Time { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }
}
