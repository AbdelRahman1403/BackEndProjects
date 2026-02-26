using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.SessionViewModel
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string TrainerName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime StartSession { get; set; }
        public DateTime EndSession { get; set; }
        public int Capacity { get; set; }
        public int AvailableSlots { get; set; }

        public string TimeDisplay => $"{StartSession:hh:mm tt} - {EndSession:hh:mm tt}";
        public string DateDisplay => StartSession.ToString("MMMM dd, yyyy");

        public TimeSpan SessionDuration => EndSession - StartSession;

        public string Status
        {
            get
            {
                var TimeNow = DateTime.Now;
                if(TimeNow < StartSession)
                {
                    return "Upcoming";
                }
                else if(TimeNow >= StartSession && TimeNow <= EndSession)
                {
                    return "Ongoing";
                }
                else
                {
                    return "Completed";
                }
            }
        }
    }
}
