using AutoMapper;
using Core.DomainLayer.Models.IdentityModels;
using DomainLayer.Exceptions;
using DomainLayer.Exceptions.AuthenticationExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstractionLayer.IServices;
using Shared.Dtos.AuthenticationDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ServiceLayer.Services
{
    public class AuthenticationServices(UserManager<ApplicationUser> userManager , IConfiguration configuration , IMapper mapper) : IAuthenticationServices
    {
        public async Task<bool> ChickEmailAsync(string email)
        {
            var User = await userManager.FindByEmailAsync(email);

            return User is not null;
        }
        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
            var User = await userManager.FindByEmailAsync(email);

            if(User is not null)
            {
                return new UserDto()
                {
                    Email = email,
                    DisplayName = User.DisplayName,
                    Token = null
                };
            }
            else
            {
                throw new UserNotFoundException(email);
            }
        }
        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var UserAddress = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(ch => ch.Email == email);

            if(UserAddress.Address is null)
            {
                throw new AddressNotFoundException(email);
            }
            else
            {       
                return mapper.Map<Address,AddressDto>(UserAddress.Address);
            }
        }
        public async Task<AddressDto> UpdateUserAddressAsync(string email, AddressDto address)
        {
            var UserAddress = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(ch => ch.Email == email) ?? throw new UserNotFoundException(email);
            
            if(UserAddress.Address is not null)
            {
                UserAddress.Address.FirstName = address.FirstName;
                UserAddress.Address.LastName = address.LastName;
                UserAddress.Address.Street = address.Street;
                UserAddress.Address.City = address.City;
                UserAddress.Address.Country = address.Country;
            }
            else
            {
                UserAddress.Address = mapper.Map<AddressDto, Address>(address);
            }
            await userManager.UpdateAsync(UserAddress);

            return mapper.Map<Address, AddressDto>(UserAddress.Address);
        }

        public async Task<UserDto> LoginAsync(LoginDto dto)
        {
            var User = await userManager.FindByEmailAsync(dto.Email) ?? throw new UserNotFoundException(dto.Email);

            var CheckPassword = await userManager.CheckPasswordAsync(User, dto.Password);
            if (CheckPassword)
            {
                return new UserDto()
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email,
                    Token = await GenerateTokenAsync(User)
                };
            }
            else
            {
                throw new unAuthenticationException();
            }
        }
        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            var User = new ApplicationUser()
            {
                DisplayName = dto.DisplayName,
                Email = dto.Email,
                UserName = dto.UserName,
                PhoneNumber = dto.PhoneNumber,
            };
            
            var Result = await userManager.CreateAsync(User , dto.Password);

            if (Result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = dto.DisplayName,
                    Email = dto.Email,
                    Token = await GenerateTokenAsync(User)
                };
            }
            else
            {
                var Errors = Result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(Errors);
            }
        }
        private async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var UserClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            var Roles = await userManager.GetRolesAsync(user);
            foreach(var role in Roles)
            {
                UserClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecurityKey = configuration.GetSection("JwtOptions")["SecurityKey"];

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
            var Creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                    issuer : configuration.GetSection("JwtOptions")["Issuer"], // ==> بيحدد الجهة اللي صدرت منه التوكين
                    audience: configuration.GetSection("JwtOptions")["Audience"], // ==> بيحدد الجهة اللي التوكين موجه لها
                    claims : UserClaims,
                    expires : DateTime.Now.AddDays(2),
                    signingCredentials : Creds
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
