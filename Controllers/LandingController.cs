using Dicer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dicer.Interfaces;

namespace Dicer.Controllers
{
    [AllowAnonymous]
    public class LandingController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LandingController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LandingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null && !user.EmailConfirmed && (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Konfimasi Email");
                    return View(model);
                }
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    if(await _userManager.IsInRoleAsync(user, Constants.Constants.roleNameClient))
                    {
                        //client
                        return RedirectToAction("HomeClient", "home");
                    }
                    else
                    {
                        //creator
                        return RedirectToAction("HomeCreator", "home");
                    }                 
                }

                ModelState.AddModelError(string.Empty, "Email atau Password Salah");
            }
            return View(model);
        }
    }
}
