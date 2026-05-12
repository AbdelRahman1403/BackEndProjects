using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer.IServices;
using Shared.Dtos.AuthenticationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto dto)
        {
            var user = await serviceManager.AuthenticationServices.LoginAsync(dto);

            return Ok(user);
        }
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto dto)
        {
            var user = await serviceManager.AuthenticationServices.RegisterAsync(dto);

            return Ok(user);
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await serviceManager.AuthenticationServices.ChickEmailAsync(email);
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await serviceManager.AuthenticationServices.GetCurrentUserAsync(Email);
            return Ok(user);
        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var userAddress = await serviceManager.AuthenticationServices.GetCurrentUserAddressAsync(Email);
            return Ok(userAddress);
        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto dto)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var userAddress = await serviceManager.AuthenticationServices.UpdateUserAddressAsync(Email , dto);
            return Ok(userAddress);
        }
    }
}
