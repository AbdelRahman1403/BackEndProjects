using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.MemberShipViewModels
{
    public class MemberShipToCreateViewModel
    {
        [Required(ErrorMessage = "Plan is Required")]
        [Display(Name = "Plan Name")]
        public int PlanId { get; set; }
        [Required(ErrorMessage = "Member is Required")]
        [Display(Name = "Member Name")]
        public int MemberId { get; set; }
        //[DisplayFormat(DataFormatString = "{0:yy,MM dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Start Membership")]
        //public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:yy,MM dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Membership")]
        public DateTime EndDate { get; set; }
    }
}
