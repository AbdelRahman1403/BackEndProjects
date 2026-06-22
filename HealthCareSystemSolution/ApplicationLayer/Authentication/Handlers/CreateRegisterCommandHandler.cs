using ApplicationLayer.Authentication.Commands;
using ApplicationLayer.Entities;
using ApplicationLayer.Entities.Enums;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Interfaces.Repositories;
using ApplicationLayer.Interfaces.UOW;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Authentication.Handlers
{
    public class CreateRegisterCommandHandler(IRegistrationSessionRepository repo , UserManager<ApplicationUser> UserManager) : IRequestHandler<CreateRegisterCommand>
    {
        public async Task Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
        {
            var session = await  repo.GetRegisterSessionAsync(request.UserDto.Email);
            if (session is null || (session.RegistrationToken != request.RegistrationToken))
                throw new BadRequestException("Invalid registration token.");

            var applicationUser = new ApplicationUser()
            {
                FirstName = request.UserDto.FirstName,
                LastName = request.UserDto.LastName,
                UserName = request.UserDto.UserName,
                gender = (Gender)request.UserDto.Gender,
                Email = request.UserDto.Email,
                EmailConfirmed = true,
                IsActive = true,
            };

            var Result = await UserManager.CreateAsync(applicationUser, request.UserDto.Password);

            if(!Result.Succeeded)
                throw new BadRequestException("User creation failed.", Result.Errors.Select(e => e.Description));

            var result = await UserManager.AddToRoleAsync(applicationUser, "Patient");

            if(!result.Succeeded)
                throw new BadRequestException("Adding user to role failed.", result.Errors.Select(e => e.Description));

            await repo.DeleteRegisterSessionAsync(request.RegistrationToken);
        }
    }
}
