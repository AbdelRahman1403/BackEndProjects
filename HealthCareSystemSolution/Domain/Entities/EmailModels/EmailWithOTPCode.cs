using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Entities.EmailModels
{
    public class EmailWithOTPCode
    {
        public string Email { get; set; } = null!;
        public string OTPCode { get; set; } = null!;
        public bool isVerified { get; set; } = false;
        public DateTime ExpirationTime { get; set; }
    }
}
