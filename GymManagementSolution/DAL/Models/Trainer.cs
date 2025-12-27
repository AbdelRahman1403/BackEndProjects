using DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Trainer : GymUser
    {
        // Join Date table
        public Specialties specialties { get; set; }
        public ICollection<Session> Session { get; set; }
    }
}
