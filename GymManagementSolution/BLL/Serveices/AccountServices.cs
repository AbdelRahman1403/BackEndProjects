using BLL.Interfaces;
using BLL.ViewModels.AccountViewModels;
using DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Serveices
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public ApplicationUser? ValidateLogin(AccountViewModel accountViewModel)
        {
            var user = _userManager.FindByEmailAsync(accountViewModel.Email).Result;
            if (user is null)
                return null;

            var IsPasswordValid = _userManager.CheckPasswordAsync(user, accountViewModel.Password).Result;

            return IsPasswordValid ? user : null;
        }
    }
}
