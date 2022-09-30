using Dicer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dicer.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(Register model, string submit)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = model.Email, UserName = model.Email, Name = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);

                switch (submit)
                {
                    //add role client
                    case "submitClient":
                        {
                            if (result.Succeeded)
                            {
                                var role = await _roleManager.FindByIdAsync(Constants.Constants.roleIdClient);
                                await _userManager.AddToRoleAsync(user, role.Name);
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                // redirect to client home page
                                return RedirectToAction("index", "home");
                            }
                            break;
                        }

                    //add role creator
                    case "submitCreator":
                        {
                            if (result.Succeeded)
                            {
                                var role = await _roleManager.FindByIdAsync(Constants.Constants.roleIdCreator);
                                await _userManager.AddToRoleAsync(user, role.Name);
                                await _signInManager.SignInAsync(user, isPersistent: false);
                                //redirect to creator home page
                                return RedirectToAction("index", "home");
                            }
                            break;
                        }
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            /*var userData = await GetCurrentUserAsync();
            var userPekerjaan = userData.Pekerjaan;*/
            return View(model);

        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
