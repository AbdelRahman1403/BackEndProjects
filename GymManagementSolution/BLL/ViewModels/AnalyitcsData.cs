using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class AnalyitcsData
    {
        public int TotalActiveMembers { get; set; }
        public int TotalMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int OnGoingSessions { get; set; }
        public int UpcomingSessions { get; set; }
        public int CompletedSessions { get; set; }
    }
}
