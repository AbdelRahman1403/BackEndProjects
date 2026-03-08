using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.MemberShipViewModels
{
    public class MemberShipViewModel
    {
        public int PlanId { get; set; }
        public int MemberId { get; set; }
        public string Member { get; set; } = null!;
        public string Plan { get; set; } = null!;
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
    }
}
