using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OrderDtos
{
    public class DeliveryMethodDto
    {
        int Id { get; set; }
        public string ShortName { get; set; } = null!;
        public string Descripiton { get; set; } = null!;
        public string DeliveryTime { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
