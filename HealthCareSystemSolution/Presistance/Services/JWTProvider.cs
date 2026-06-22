using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Dtos;
using Shared.OptionsModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class JWTService : IJWTService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWTSettingsOptions _jwtSettings;
        public JWTService(UserManager<ApplicationUser> userManager , IOptions<JWTSettingsOptions> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponseDto> GenerateUserTokenAsync(ApplicationUser user)
        {
            var UserRoles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.Name , user.UserName!),
            };

            claims.AddRange(UserRoles.Select(roles => new Claim(ClaimTypes.Role , roles)));

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.key));

            var Credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Expires = DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: Expires,
                signingCredentials: Credentials
            );

            var TokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResponseDto
            {
                Token = TokenString,
                Expiration = Expires,
                UserName = user.UserName!,
                Email = user.Email!
            };
        }
    }
}
