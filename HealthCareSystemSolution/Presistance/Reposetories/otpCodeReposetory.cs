using ApplicationLayer.Entities.EmailModels;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Interfaces.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Reposetories
{
    public class otpCodeReposetory(IConnectionMultiplexer connection) : IotpCodeReposetory
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task SetOTPCodeAsync(EmailWithOTPCode verify, TimeSpan expiration)
        {
            var OtpCodeJson = JsonSerializer.Serialize(verify);
            var isAdded = await _database.StringSetAsync(verify.Email, OtpCodeJson, expiration, when: When.NotExists);
            if(!isAdded)
            {
                throw new Exception("OTP code already exists for this email.");
            }
        }

        public async Task DeleteOTPCodeAsync(string email) =>  await _database.KeyDeleteAsync(email);

        public async Task<EmailWithOTPCode?> GetOTPCodeAsync(string email)
        {
            var OtpCode = await _database.StringGetAsync(email);

            return OtpCode.IsNullOrEmpty ? null : JsonSerializer.Deserialize<EmailWithOTPCode>(OtpCode);
        }

        public async Task<bool> CheckOTPCodeAsync(string email, string code)
        {
            var result = await GetOTPCodeAsync(email);

            if (result == null || (result.OTPCode != code && result.Email != email))
            {
                return false;
            }
            return true;
        }
    }
}
