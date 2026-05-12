using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.AuthenticationExceptions
{
    public class UserNotFoundException(string Email) : NotFoundException($"User with email '{Email}' was not found.")
    {
    }
}
