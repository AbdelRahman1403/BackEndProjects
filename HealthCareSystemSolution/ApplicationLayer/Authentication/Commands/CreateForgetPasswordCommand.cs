using MediatR;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Authentication.Commands
{
    public sealed record CreateForgetPasswordCommand(ForgetPasswordDto UserDto , string ResetSession) : IRequest;
}
