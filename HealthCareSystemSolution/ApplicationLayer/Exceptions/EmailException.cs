using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Exceptions
{
    public sealed class EmailException : AppException
    {
        public EmailException(string message)
            : base(message, StatusCodes.Status503ServiceUnavailable)
        {
        }
    }
}
