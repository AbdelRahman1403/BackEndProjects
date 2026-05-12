using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.OrdersException
{
    public class DeliveryMethodNotFoundException(int id) : NotFoundException($"The Method of Delivery with Id {id} is not found")
    {
    }
}
