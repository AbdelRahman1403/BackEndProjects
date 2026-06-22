using ApplicationLayer.Entities.EmailModels;
using ApplicationLayer.Interfaces.Repositories;
using DomainLayer.Entities.AuthenticationModels;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Reposetories
{
    public class RegistrationSessionRepository(IConnectionMultiplexer connection) : IRegistrationSessionRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task CreateRegisterSessionAsync(RegistrationSession session, TimeSpan duration)
        {
            var RegisterSessionJson = JsonSerializer.Serialize(session);
            var isAdded = await _database.StringSetAsync(session.Email, RegisterSessionJson, duration, when: When.NotExists);
            if (!isAdded)
            {
                throw new Exception("OTP code already exists for this email.");
            }
        }

        public async Task CreateResetPasswordSessionAsync(ResetSession session, TimeSpan duration)
        {
            var ResetPasswordSession = JsonSerializer.Serialize(session);
            var isAdded = await _database.StringSetAsync(session.Email, ResetPasswordSession, duration, when: When.NotExists);
            if (!isAdded)
            {
                throw new Exception("OTP code already exists for this email.");
            }
        }

        public async Task DeleteRegisterSessionAsync(string email) => await _database.KeyDeleteAsync(email);

        public async Task<RegistrationSession?> GetRegisterSessionAsync(string email)
        {
            var sessionJson = await _database.StringGetAsync(email);

            return sessionJson.IsNullOrEmpty ? null : JsonSerializer.Deserialize<RegistrationSession>(sessionJson);
        }

        public async Task<ResetSession?> GetResetPasswordSessionAsync(string email)
        {
            var sessionJson = await _database.StringGetAsync(email);

            return sessionJson.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ResetSession>(sessionJson);
        }
    }
}
