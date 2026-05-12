using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.AuthenticationExceptions
{
    public class AddressNotFoundException(string email) : NotFoundException($"Address for user with email {email} not found.")
    {
    }
}
