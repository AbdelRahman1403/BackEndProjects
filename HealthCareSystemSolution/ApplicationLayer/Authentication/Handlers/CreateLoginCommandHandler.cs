using ApplicationLayer.Authentication.Commands;
using ApplicationLayer.Entities;
using ApplicationLayer.Exceptions.AuthenticationExceptions;
using ApplicationLayer.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Authentication.Handlers
{
    public class CreateLoginCommandHandler(IServiceManager serviceManager , UserManager<ApplicationUser> userManager) : IRequestHandler<CreateLoginCommand, AuthResponseDto>
    {
        public async Task<AuthResponseDto> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByNameAsync(request.dto.UserName);

            if(user is null) 
                 throw new UserNotFoundException($"User with username {request.dto.UserName} not found.");

            var isPasswordValid = await userManager.CheckPasswordAsync(user, request.dto.Password);
            if (!isPasswordValid)
                throw new Exception("Invalid password.");

            return await serviceManager.JWTService.GenerateUserTokenAsync(user);
        }
    }
}
