using Shared.Dtos.AuthenticationDtos;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OrderDtos
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public DateTimeOffset timeOffset { get; set; } 
        public AddressDto Address { get; set; } = null!;
        public string Status { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = [];
        public string deliveryMethod { get; set; } = null!;
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
