using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.MemberViewModels
{
    public class MemberToUpdateViewModel
    {
        [StringLength(70, MinimumLength = 2, ErrorMessage = "The Name Length must between 2 and 70")]
        [RegularExpression(@"^[a-zA-Z\s]+&", ErrorMessage = "Please Re-Write the name")]
        public string Name { get; set; } = null!;

        [StringLength(100, MinimumLength = 10, ErrorMessage = "The Email Length must between 10 and 100")]
        [EmailAddress(ErrorMessage = "Please Re-Write the Email")]
        public string Email { get; set; } = null!;
        [RegularExpression(@"^[010|011|012|015]\d{8}$")]
        public string PhoneNumber { get; set; } = null!;
        public string? Photo { get; set; }

        #region Address

        public string? Street { get; set; }
        public string? City { get; set; }
        [Range(1, 10000, ErrorMessage = "Please Re-Write BuildingNumber")]
        public string? BuildingNumber { get; set; }
        #endregion
        
    }
}
