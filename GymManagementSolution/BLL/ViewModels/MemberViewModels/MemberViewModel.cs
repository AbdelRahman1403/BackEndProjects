using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.MemberViewModels
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string? Photo { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Gender { get; set; } = null!;

        //---------------------------

        public string? PlanName{ get; set; }
        public string? BirthOfDate { get; set; }
        public string? MemberShipStartDate { get; set; }
        public string? MemberShipEndDate { get; set; }
        public string? Address { get; set; }
    }
}
