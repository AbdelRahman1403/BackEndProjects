using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.PlanViewModels
{
    public class UpdateToPlanViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "The Name Length must between 3 and 10")]
        public string PlanName { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(25, MinimumLength = 10, ErrorMessage = "The Description Length must between 10 and 25")]
        public string PlanDescription { get; set; } = null!;
        [Required(ErrorMessage = "Price is required")]
        [Range(500, 7000, ErrorMessage = "Price must be between 500 and 7000")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Duration days of plan is required")]
        [Range(7, 365, ErrorMessage = "DurationInDays must be between 7 and 365")]
        public int DurationInDays { get; set; }
    }
}
