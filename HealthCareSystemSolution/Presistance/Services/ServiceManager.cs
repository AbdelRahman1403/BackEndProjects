using ApplicationLayer.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IEmailService> _emailService;
        private readonly Lazy<IOTPService> _otpService;
        private readonly Lazy<IJWTService> _jWTService;
        private readonly Lazy<IForgotPasswordService> _forgotPasswordService;
        public ServiceManager(IServiceProvider serviceProvider)
        {
            _emailService = new Lazy<IEmailService>(
                serviceProvider.GetRequiredService<IEmailService>);         
            _forgotPasswordService = new Lazy<IForgotPasswordService>(
                serviceProvider.GetRequiredService<IForgotPasswordService>);
            _otpService = new Lazy<IOTPService>(
                serviceProvider.GetRequiredService<IOTPService>);

            _jWTService = new Lazy<IJWTService>(
                serviceProvider.GetRequiredService<IJWTService>);
        }

        public IEmailService EmailService => _emailService.Value;
        public IOTPService OTPService => _otpService.Value;
        public IJWTService JWTService => _jWTService.Value;

        public IForgotPasswordService ForgotPasswordService => _forgotPasswordService.Value;
    }
}
