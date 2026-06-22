using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendOTPCode(string email);
        //Task<bool> VerifyOTPCode(string email, string code);
        Task SendEmailAsync(string email, string subject, string body);

    }
}
