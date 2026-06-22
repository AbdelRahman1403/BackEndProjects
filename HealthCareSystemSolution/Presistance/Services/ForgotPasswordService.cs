using ApplicationLayer.Entities;
using ApplicationLayer.Exceptions.AuthenticationExceptions;
using ApplicationLayer.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Persistence.Reposetories;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class ForgotPasswordService: IForgotPasswordService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOTPService _oTPService;
        private readonly IEmailService _emailService;

        public ForgotPasswordService(UserManager<ApplicationUser> userManager , IOTPService oTPService , IEmailService emailService)
        {
            _userManager = userManager;
            _oTPService = oTPService;
            _emailService = emailService;
        }
        public async Task SendOTPCodeAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null || !user.IsActive)
            {
                throw new UserNotFoundException("User not found or is not active in the system.");
            }
            var otpCode = await _oTPService.GenerateOTPCode(email);

            var subject = "Forget Password";
            var body = $"""
                Hello,

                We received a request to reset your password.

                Please use the following One-Time Password (OTP) to complete the password reset process:

                OTP Code: {otpCode}

                This code is valid for 5 minutes and can only be used once.

                If you did not request a password reset, please ignore this email. No changes will be made to your account.

                Best regards,
                Health Care System Team
                """;

            await _emailService.SendEmailAsync(email,subject , body);
        }

        public async Task<string> VerifyResetPasswordAsync(string email, string otpCode)
        {
             var result = await _oTPService.VerifyResetPasswordOTPCodeAsync(email, otpCode);

            if(result is null)
            {
                throw new Exception("Invalid OTP Number");
            }
            return result;
        }
    }
}
