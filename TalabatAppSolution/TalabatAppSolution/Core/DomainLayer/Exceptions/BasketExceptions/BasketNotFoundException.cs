using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.BasketExceptions
{
    public class BasketNotFoundException(string Key) : NotFoundException($"Basket with key {Key} not found")
    {
    }
}
