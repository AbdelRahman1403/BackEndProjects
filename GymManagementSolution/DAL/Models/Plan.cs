using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Plan : BaseEntity
    {
        public string PlanName { get; set; } = null!;
        public string PlanDescription { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public bool IsActive { get; set; }

        public ICollection<MemberShip> MemberShips { get; set; } = null!;

    }
}
