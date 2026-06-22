using ApplicationLayer.Entities;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces.Services
{
    public interface IJWTService
    {
         Task<AuthResponseDto> GenerateUserTokenAsync(ApplicationUser user);
    }
}
