using ApplicationLayer.Entities.Enums;
using ApplicationLayer.Entities.MedicalStuffModels.DoctorModels;
using ApplicationLayer.Entities.PatientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities.AppointmentsModels
{
    public class Appointment : BaseEntity
    { 
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int ReservationTimeId { get; set; }
        public ReservationTime AppointmentDateTime { get; set; }
        public AppointmentsStatus Status { get; set; }
        public string Report { get; set; } = null!;
    }
}
