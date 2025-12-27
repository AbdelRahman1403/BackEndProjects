using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class MemberShip : BaseEntity
    {
        public string Status
        {
            get
            {
                if(EndDate >= DateTime.Now)
                {
                    return "Active";
                }
                else
                {
                    return "Expired";
                }
            }
        }
        public DateTime EndDate { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
    }
}
