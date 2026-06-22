using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer.IServices
{
    public interface IAuthenticationServices
    {
        public Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        public Task<AuthResponseDto> LoginAsync(LoginDto dto);
        public Task<VerifyOTPResponseDto> CheckOtpAsync(string email , int otpCode);
    }
}
