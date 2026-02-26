using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.SessionViewModel
{
    public class CreateSessionViewModel
    {
        [Required(ErrorMessage ="Category is Required")]
        [Display(Name ="Category Name")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Trainer is Required")]
        [Display(Name = "Trainer Name")]
        public int TrainerId { get; set; }
        [DisplayFormat(DataFormatString = "{0:yy,MM dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Start Session time & date")]
        public DateTime StartSession { get; set; }
        [DisplayFormat(DataFormatString = "{0:yy,MM dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Session time & date")]
        public DateTime EndSession { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        [StringLength(50 , MinimumLength = 10, ErrorMessage = "The Description Length must between 10 and 50")]
        public string Description { get; set; } = null!;
        [Range(5 , 25 , ErrorMessage ="Capacity must be between 5 and 25")]
        public int Capacity { get; set; }
    }
}
