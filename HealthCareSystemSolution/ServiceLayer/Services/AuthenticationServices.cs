using DomainLayer.Entities;
using DomainLayer.Entities.Enums;
using DomainLayer.Exceptions.AuthenticationExceptions;
using DomainLayer.GenericReposetories;
using Microsoft.AspNetCore.Identity;
using ServiceAbstractionLayer.IServices;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> userManager , IVerifyOTPReposetory oTPReposetory) : IAuthenticationServices
    {
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser()
            {
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                gender = Enum.TryParse<Gender>(dto.Gender.ToString(), out var gender) ? gender : Gender.Male,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                CreatedAt = DateTime.Now
            };

            var Result = await userManager.CreateAsync(user, dto.Password);
            if (Result.Succeeded)
            {

            }

        }
        public Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<VerifyOTPResponseDto> CheckOtpAsync(string email , int otpCode)
        {
            var Result = await oTPReposetory.GetOTPCodeAsync(email);
            if (Result == null)
            {
                throw new VerifiyOTPException("Invalid Email");
            }
            if (Result.OTPCode != otpCode)
            {
                throw new VerifiyOTPException("Invalid OTP code");
            }
            if (Result.ExpireAt < DateTime.Now)
            {
                throw new VerifiyOTPException("OTP code expired");
            }
            await oTPReposetory.DeleteOTPCodeAsync(email);
            return new VerifyOTPResponseDto()
            {
                Message = "OTP code verified successfully",
                IsVerified = true
            };
        }
    }
}
