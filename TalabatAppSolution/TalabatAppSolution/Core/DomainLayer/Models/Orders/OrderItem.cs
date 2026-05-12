using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models.Orders
{
    public class OrderItem : BaseEntity<int>
    {
        public decimal Priced { get; set; }
        public int Quentity { get; set; }

        public ProductItemOrdered ProductItem { get; set; }

    }
}
