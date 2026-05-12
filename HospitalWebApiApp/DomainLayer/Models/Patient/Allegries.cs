using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Patient
{
    public class Allergies : BaseEntity<int>
    {
        public string AllergyName { get; set; } = null!;
    }
}
