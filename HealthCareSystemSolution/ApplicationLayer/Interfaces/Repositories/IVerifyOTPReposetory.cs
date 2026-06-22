using ApplicationLayer.Entities.EmailModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Repositories
{
    public interface IotpCodeReposetory
    {
        Task<bool> CheckOTPCodeAsync(string email, string code);
        Task<EmailWithOTPCode?> GetOTPCodeAsync(string email);
        Task SetOTPCodeAsync(EmailWithOTPCode verify, TimeSpan expiration);
        Task DeleteOTPCodeAsync(string email);
    }
}
