using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.ProductExceptions
{
    public class ProductException(int id) : NotFoundException($"The product with id : {id} is not found")
    {
    }
}
