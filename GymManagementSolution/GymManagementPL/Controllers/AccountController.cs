using BLL.Interfaces;
using BLL.ViewModels.AccountViewModels;
using DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountServices accountServices , 
                                 SignInManager<ApplicationUser> signInManager)
        {
            _accountServices = accountServices;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AccountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Email or password");
                return View(model);
            }

            var user = _accountServices.ValidateLogin(model);
            if (user is null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid User In System");
                return View(model);
            }
            
            var result = _signInManager.PasswordSignInAsync(user , model.Password, model.RememberMe , false).Result;

            if (result.IsNotAllowed)
            {
                ModelState.AddModelError("InvalidLogin", "The user is not allowed");
            }
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("InvalidLogin", "The user is locked from the system , please go to hell");
            }
            if(result.Succeeded)
            {
                return RedirectToAction("Index","Home");
            }
            return View(model);
        }
    }
}
