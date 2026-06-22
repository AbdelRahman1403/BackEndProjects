using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Exceptions.AuthenticationExceptions
{
    public sealed class UserNotFoundException: AppException
    {
        public UserNotFoundException(string Message) : base(Message , StatusCodes.Status400BadRequest)
        {
            
        }
    }
}
