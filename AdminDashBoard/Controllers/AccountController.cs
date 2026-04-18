using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Route.Domains.Entities.Identity;
using Store.Route.Services.Abstractions.Auth;

namespace AdminDashBoard.Controllers
{
    public class AccountController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager)
        : Controller
    {

        [HttpGet]
        public IActionResult Login() => View();


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                ModelState.AddModelError("", "Invalid login attempt. Please check your email and password.");
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    return RedirectToAction(nameof(Index), "Home");
                else
                {
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError("", "You are not authorized to access the Admin Panel.");
                    return View(model);
                }
            }
            ModelState.AddModelError("", "Invalid login attempt. Please check your email and password.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }


    }
}