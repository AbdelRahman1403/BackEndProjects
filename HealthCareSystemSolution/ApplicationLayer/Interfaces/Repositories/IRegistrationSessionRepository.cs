using DomainLayer.Entities.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Repositories
{
    public interface IRegistrationSessionRepository
    {
        Task CreateResetPasswordSessionAsync(ResetSession session, TimeSpan duration);
        Task CreateRegisterSessionAsync(RegistrationSession session , TimeSpan duration);
        Task<RegistrationSession?> GetRegisterSessionAsync(string email);
        Task<ResetSession?> GetResetPasswordSessionAsync(string email);
        Task DeleteRegisterSessionAsync(string email);
    }
}
