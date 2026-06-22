using ApplicationLayer.Authentication.Commands;
using ApplicationLayer.Entities;
using ApplicationLayer.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Authentication.Handlers
{
    public class CreateForgetPasswordHandler(IRegistrationSessionRepository repo, UserManager<ApplicationUser> UserManager) : IRequestHandler<CreateForgetPasswordCommand>
    {
        public Task Handle(CreateForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
