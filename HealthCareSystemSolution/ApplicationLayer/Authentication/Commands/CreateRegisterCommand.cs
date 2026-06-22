using ApplicationLayer.Entities;
using DomainLayer.Entities.AuthenticationModels;
using MediatR;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Authentication.Commands
{
    public sealed record CreateRegisterCommand(RegisterDto UserDto , string RegistrationToken) : IRequest;

}
