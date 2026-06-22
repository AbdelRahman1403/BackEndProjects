using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Exceptions
{
    public sealed class BadRequestException : AppException
    {
        public IEnumerable<string?> Errors { get; }
        public IEnumerable<string> Enumerable { get; }

        public BadRequestException(string message , IEnumerable<string?> errors = null)
            : base(message, StatusCodes.Status400BadRequest)
        {
            Errors = errors;
        }

    }
}
