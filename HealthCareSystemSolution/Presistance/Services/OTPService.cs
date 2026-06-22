using ApplicationLayer.Entities.EmailModels;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Interfaces.Repositories;
using ApplicationLayer.Interfaces.Services;
using DomainLayer.Entities.AuthenticationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace Persistence.Services
{
    public class OTPService : IOTPService
    {
        private readonly IotpCodeReposetory _repo;
        private readonly IRegistrationSessionRepository _registarRepo;

        public OTPService(IotpCodeReposetory repo , IRegistrationSessionRepository RegistarRepo)
        {
            _repo = repo;
            _registarRepo = RegistarRepo;
        }
        public Task<string> GenerateOTPCode(string email)
        {
            string otpCode = GenerateOTP();

            var EmailOTP = new EmailWithOTPCode()
            {
                Email = email,
                OTPCode = otpCode,
                ExpirationTime = DateTime.UtcNow.AddMinutes(5)
            };
            _repo.SetOTPCodeAsync(EmailOTP , TimeSpan.FromMinutes(5));
            return Task.FromResult(otpCode);
        }

        public async Task<bool> VerifyOTPCodeAsync(string email, string code)
        {
            var result = await _repo.GetOTPCodeAsync(email);

            if (result == null)
                throw new NotFoundException("OTP code not found for the provided email.");
            if(result.OTPCode != code || DateTime.UtcNow > result.ExpirationTime)
                throw new NotFoundException("OTP code is wrong or Expiration Date is out");
            await _repo.DeleteOTPCodeAsync(email);
            //if (IsRegisteration)
            //{
            //    var RegisterSession = new RegistrationSession()
            //    {
            //        Email = email,
            //        RegistrationToken = Guid.NewGuid().ToString(),
            //        CreatedAt = DateTime.UtcNow,
            //        ExpireAt = DateTime.UtcNow.AddMinutes(15)
            //    };

            //    await _registarRepo.CreateRegisterSessionAsync(RegisterSession, TimeSpan.FromMinutes(15));
            //}
            return true;
        }

        public async Task<string> VerifyOTPCodeRegistrationAsync(string email, string code)
        {
            var IsValid = await VerifyOTPCodeAsync(email, code);
            if (IsValid)
            {
                var RegisterSession = new RegistrationSession()
                {
                    Email = email,
                    RegistrationToken = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    ExpireAt = DateTime.UtcNow.AddMinutes(15)
                };

                await _registarRepo.CreateRegisterSessionAsync(RegisterSession, TimeSpan.FromMinutes(15));
                return RegisterSession.RegistrationToken;
            }
            return null;
        }

        public async Task<string> VerifyResetPasswordOTPCodeAsync(string email, string otpCode)
        {
            var IsValid = await VerifyOTPCodeAsync(email, otpCode);
            if (IsValid)
            {
                var resetSession = new ResetSession()
                {
                    Email = email,
                    ResetToken = Guid.NewGuid().ToString(),
                    CreatedAt = DateTime.UtcNow,
                    ExpireAt = DateTime.UtcNow.AddMinutes(5)
                };

                await _registarRepo.CreateResetPasswordSessionAsync(resetSession, TimeSpan.FromMinutes(5));
                return resetSession.ResetToken;
            }
            return null;
        }

        private string GenerateOTP() =>
            RandomNumberGenerator.GetInt32(100000, 999999).ToString();
    }
}
