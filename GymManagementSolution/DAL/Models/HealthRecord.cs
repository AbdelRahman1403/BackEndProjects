using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class HealthRecord : BaseEntity
    {
        public required decimal Weight { get; set; }
        public required decimal Height { get; set; }
        public  string BloodType { get; set; }

        public string? Notes { get; set; }

    }
}
