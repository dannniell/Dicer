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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService emailService;

        public LandingController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<IdentityRole> roleManager,
                                IEmailService emailService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this.emailService = emailService;
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
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    string body = "<th><a href=\"google.com\">Masuk ke Dicer</a></th>";
                    emailService.SendEmail("danielalferian9@gmail.com", "testing", body);
                    // redirect to client home page
                    return RedirectToAction("index", "home");
                }

                ModelState.AddModelError(string.Empty, "Gagal Login");
            }
            return View(model);
        }
    }
}
