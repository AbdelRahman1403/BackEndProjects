using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.PlanViewModels
{
    public class PlanViewModel
    {
        public int Id { get; set; }
        public string PlanName { get; set; } = null!;
        public string PlanDescription { get; set; } = null!;
        public decimal Price { get; set; }
        public int DurationInDays { get; set; }
        public bool IsActive { get; set; }
    }
}
