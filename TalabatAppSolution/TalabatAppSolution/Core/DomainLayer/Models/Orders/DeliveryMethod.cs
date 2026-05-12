using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Orders
{
    public class DeliveryMethod : BaseEntity<int>
    {
        public string ShortName { get; set; } = null!;
        public string Descripiton { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
