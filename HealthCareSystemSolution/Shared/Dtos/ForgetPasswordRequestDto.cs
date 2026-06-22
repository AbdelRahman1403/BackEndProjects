using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ForgetPasswordRequestDto
    {
        public ForgetPasswordDto forgetPasswordDto { get; set; } = null!;
        public string resetSessionToken { get; set; } = null!;
    }
}
