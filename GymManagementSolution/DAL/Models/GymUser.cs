using DAL.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class GymUser : BaseEntity
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public Gender gender { get; set; }
        public Address address { get; set; }
    }
    [Owned]
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string BuildingNumber { get; set; }
    }
}
