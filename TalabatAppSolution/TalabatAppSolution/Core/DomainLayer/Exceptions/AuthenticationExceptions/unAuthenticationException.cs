using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Exceptions.AuthenticationExceptions
{
    public class unAuthenticationException(string Message = "Invalid Email or Password") : Exception(Message)
    {
    }
}
