using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SperingTask.Models;
using SperingTask.ViewModels.Account;

namespace SperingTask.Controllers
{
    public class AccountController(UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            AppUser user = new AppUser
            {
                Email = vm.Email,
                Name = vm.Name,
                Surname = vm.Surname,
                UserName = vm.Username
            };
            IdentityResult result = await _userManager.CreateAsync(user, vm.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(vm);
            }

            return RedirectToAction(nameof(Index), "Home");
        }

        public  IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid) return View(vm);
            AppUser? user = await _userManager.FindByNameAsync(vm.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(vm.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "ISDIFADECI ADI VE YA SIFRE YANLISDI");
                    return View(vm);
                }
            }
            await _signInManager.CheckPasswordSignInAsync(user,vm.Password,true)
          
            var result= await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);
            if (result.IsLockedOut) 
            {
                ModelState.AddModelError("", "cox saydayanlis deyer gonderdiz ,zehmet olmasa gozleyin" + user.LockoutEnd.Value.AddHours(4).ToString("HH:mm:ss"));
                return View(vm);
            }
            return RedirectToAction("Index", "Home");
        }
		public async Task<IActionResult>  Logout()
		{
            await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
	}
}
