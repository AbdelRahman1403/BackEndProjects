using ApplicationLayer.Entities.EmailModels;
using DomainLayer.Entities.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Services
{
    public interface IOTPService
    {
         Task<string> GenerateOTPCode(string email);
         Task<bool> VerifyOTPCodeAsync(string email, string code);
         Task<string> VerifyOTPCodeRegistrationAsync(string email, string code);
         Task<string> VerifyResetPasswordOTPCodeAsync(string email, string otpCode);
    }
}
