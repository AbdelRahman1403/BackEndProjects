using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.MemberViewModels
{
    public class HealthRecordViewModel
    {
        public int Id { get; set; }
        [Range(50 , 300 , ErrorMessage = "Please Re-Write Weight")]
        public  decimal Weight { get; set; }
        [Range(100 , 250 , ErrorMessage = "Please Re-Write Height")]
        public decimal Height { get; set; }
        [StringLength(3 , MinimumLength =1 , ErrorMessage = "The Blood Type Length must between 1 and 3")]
        public string BloodType { get; set; } = null!;

        public string? Notes { get; set; }
    }
}
