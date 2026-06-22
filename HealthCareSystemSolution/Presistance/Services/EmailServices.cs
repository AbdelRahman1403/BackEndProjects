using ApplicationLayer.Entities.EmailModels;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Interfaces.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Shared.OptionsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace Persistence.Services
{
        public class EmailServices : IEmailService
        {
            private readonly EmailSettingsOptions _emailSettings;
            private readonly IOTPService _otpService;

            public EmailServices(IOptions<EmailSettingsOptions> emailOption , IOTPService oTPService)
            {
                _emailSettings = emailOption.Value;
                _otpService = oTPService;
            }
            public async Task SendOTPCode(string email)
            {

                 var result = await _otpService.GenerateOTPCode(email);
                 var body =
                          $"Hello,\n\n" +
                          $"Your OTP code is: {result}\n" +
                          $"This code will expire in 5 minutes.\n\n" +
                          $"If you did not request this, please ignore this email.";

                 await SendEmailAsync(
                     email,
                     "Hospital Account Verification OTP",
                     body);
        }

             public async Task SendEmailAsync(string email,string subject,string body)
        {
            try
            {


                var message = new MimeMessage();

                message.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.Email));

                message.To.Add(MailboxAddress.Parse(email));

                message.Subject = subject;

                message.Body = new TextPart(TextFormat.Plain)
                {
                    Text = body
                };

                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(
                    _emailSettings.Host,
                    _emailSettings.Port,
                    SecureSocketOptions.StartTls);

                await smtp.AuthenticateAsync(
                    _emailSettings.Email,
                    _emailSettings.Password);

                await smtp.SendAsync(message);

                await smtp.DisconnectAsync(true);
            }
            catch (SmtpCommandException ex)
            {
                throw new EmailException(
                    $"SMTP command failed: {ex.Message}");
            }
            catch (SmtpProtocolException ex)
            {
                throw new EmailException(
                    $"SMTP protocol error: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new EmailException(
                    $"Unable to send email: {ex.Message}");
            }
        }

        
    }
}
