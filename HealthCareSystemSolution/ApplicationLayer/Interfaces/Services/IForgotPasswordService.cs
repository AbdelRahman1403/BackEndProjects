using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Services
{
    public interface IForgotPasswordService
    {
        Task SendOTPCodeAsync(string email);
        Task<string> VerifyResetPasswordAsync(string email, string otpCode);
    }
}
