using Shared.Dtos.AuthenticationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer.IServices
{
    public interface IAuthenticationServices
    {
        Task<UserDto> LoginAsync(LoginDto dto);

        Task<UserDto> RegisterAsync(RegisterDto dto);
        
        Task<bool> ChickEmailAsync(string email);
        Task<UserDto> GetCurrentUserAsync(string email);
        Task<AddressDto> GetCurrentUserAddressAsync(string email);

        Task<AddressDto> UpdateUserAddressAsync(string email , AddressDto address);
    }
}
