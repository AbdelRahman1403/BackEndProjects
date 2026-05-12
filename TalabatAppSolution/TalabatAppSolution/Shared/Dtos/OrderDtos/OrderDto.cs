using Shared.Dtos.AuthenticationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OrderDtos
{
    public class OrderDto
    {
        public AddressDto Address { get; set; }
        public string BasketId { get; set; }
        public int DeliveryMethodId { get; set; }

        
    }
}
