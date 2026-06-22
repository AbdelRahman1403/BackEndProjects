using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.AuthenticationModels
{
    public class RegistrationSession
    {
        public string Email { get; set; } = null!;

        public string RegistrationToken { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpireAt { get; set; }
    }
}
