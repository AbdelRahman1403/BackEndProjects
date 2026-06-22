using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class RegisterRequestDto
    {
        public RegisterDto UserDto { get; set; } = null!;

        public string RegistrationToken { get; set; } = null!;
    }
}
