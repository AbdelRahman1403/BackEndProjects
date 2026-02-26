using BLL.ViewModels.AccountViewModels;
using DAL.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAccountServices
    {
        public ApplicationUser? ValidateLogin(AccountViewModel accountViewModel);
    }
}
