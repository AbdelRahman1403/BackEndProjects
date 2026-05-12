using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OrderDtos
{
    public class OrderItemDto
    {
        public string Name { get; set; } = null!;
        public int Quentity { get; set; } 
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
    }
}
