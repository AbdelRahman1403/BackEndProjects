using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Services
{
    public interface IServiceManager
    {
        IForgotPasswordService ForgotPasswordService { get; }       
        IEmailService EmailService { get; }
        IOTPService OTPService { get; }
        IJWTService JWTService { get; }
    }
}
