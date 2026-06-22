using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Exceptions.AuthenticationExceptions
{
    public class VerifiyOTPException : AppException
    {
        public VerifiyOTPException(string Message) : base(Message , StatusCodes.Status400BadRequest)
        {
            
        }
    }
}
