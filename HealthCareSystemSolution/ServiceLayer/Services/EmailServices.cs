using DomainLayer.Entities.EmailModels;
using DomainLayer.GenericReposetories;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using ServiceAbstractionLayer.IServices;
using Shared.OptionsModels;
using System.Security.Cryptography;

namespace ServiceLayer.Services
{
    public class EmailServices : IEmailServices
    {
        private const int waitingTimeInMinutes = 3;
        private readonly IVerifyOTPReposetory _repo;
        private readonly EmailSettingsOptions _emailOptions;

        public EmailServices(IVerifyOTPReposetory repo, IOptions<EmailSettingsOptions> emailOptions)
        {
            _repo = repo;
            _emailOptions = emailOptions.Value;
        }
        public async Task SendOTPForMailAsync(string email)
        {
            var OtpCode = GenerateOTP();
            var OtpModel = new VerifyOTPCode()
            {
                Email = email,
                OTPCode= OtpCode,
                ExpireAt = DateTime.UtcNow.AddMinutes(waitingTimeInMinutes)
            };
           await _repo.SetOTPCodeAsync(OtpModel , TimeSpan.FromMinutes(waitingTimeInMinutes));

            var subject = "Hospital Account Verification OTP";

            var body = $"""
                        Hello,

                        Your OTP code is: {OtpModel.OTPCode}

                        This code will expire in 5 minutes.

                        If you did not request this email, please ignore it.

                        Regards,
                        Hospital Team
                        """;
            await SendMailAsync(email, subject, body);
        }

        private static string GenerateOTP() => RandomNumberGenerator.GetInt32(100000, 999999).ToString();

        private async Task SendMailAsync(string email, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(
                new MailboxAddress(
                    _emailOptions.DisplayName,
                    _emailOptions.Email));

            message.To.Add(MailboxAddress.Parse(email));

            message.Subject = subject;

            message.Body = new TextPart(TextFormat.Plain)
            {
                Text = body
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _emailOptions.Host,
                _emailOptions.Port,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _emailOptions.Email,
                _emailOptions.Password);

            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }
    }
}
