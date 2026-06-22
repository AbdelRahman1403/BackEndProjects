using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class VerifyOTPCodeDto
    {
        public string Email { get; set; } = null!;
        public string OTPCode { get; set; } = null!;
    }
}
