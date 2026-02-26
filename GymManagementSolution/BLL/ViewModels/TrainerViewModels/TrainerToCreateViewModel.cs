using BLL.ViewModels.MemberViewModels;
using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.TrainerViewModels
{
    public class TrainerToCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(70, MinimumLength = 2, ErrorMessage = "The Name Length must between 2 and 70")]
        [RegularExpression(@"^[a-zA-Z\s]+", ErrorMessage = "Please Re-Write the name")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"^01[0125]\d{8}$", ErrorMessage = "Please Re-Write the Phone Number")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Birth Date is required")]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender gender { get; set; }
        [Required(ErrorMessage = "Specialty is Required")]
        [EnumDataType(typeof(Specialties))]
        public Specialties specialties { get; set; }

        [Required(ErrorMessage = "Building Number Is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Building Number must be greater than 0")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "City Is Required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City must be between 2 and 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Street Is Required")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Street must be between 2 and 150 characters")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street can only contain letters, numbers, and spaces")]
        public string Street { get; set; } = null!;
    }
}
