using Dicer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dicer.Interfaces;
using Dicer.Repositories;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dicer.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IProvinsiService provinsiService;
        private readonly IEmailService emailService;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<IdentityRole> roleManager,
                                IProvinsiService provinsiService,
                                IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.provinsiService = provinsiService;
            this.emailService = emailService;
        }

        #region Register Client
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Email = model.Email, UserName = model.Email, Name = model.Name };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);
                    
                    var role = await _roleManager.FindByIdAsync(Constants.Constants.roleIdClient);
                    await _userManager.AddToRoleAsync(user, role.Name);

                    string body = String.Format("<th><a href={0}>Klik Disini Untuk Konfirmasi Email Anda</a></th>", confirmationLink);
                    emailService.SendEmail(model.Email, "Dicer Konfirmasi Email", body);

                    return RedirectToAction("RegistrationSuccess", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        #endregion Register Client

        #region Register Creator

        [HttpGet]
        public IActionResult RegisterCreator()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterCreator(RegisterCreator model)
        {
            if (ModelState.IsValid)
            {
                
                var user = new ApplicationUser { Email = model.Email, UserName = model.Email, Name = model.Name, UserNameIg = model.UsernameIg,
                                                Provinsi = model.Provinsi, Kota = model.Kota};
                //Kota = model.Kota, Provinsi = model.Provinsi};
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token = token }, Request.Scheme);

                    var role = await _roleManager.FindByIdAsync(Constants.Constants.roleIdCreator);
                    await _userManager.AddToRoleAsync(user, role.Name);

                    string body = String.Format("<th><a href={0}>Klik Disini Untuk Konfirmasi Email Anda</a></th>", confirmationLink);
                    emailService.SendEmail(model.Email, "Dicer Konfirmasi Email", body);

                    return RedirectToAction("RegistrationSuccess", "Account");
                }
                
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        #endregion Register Creator

        [HttpGet]
        public IActionResult RegistrationSuccess()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ErrorView()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId == null || token == null)
            {
                return RedirectToAction("index", "landing");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                ViewBag.ErrorMessage = "Invalid User";
                return RedirectToAction("ErrorView", "Account");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                return View();
            }
            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return RedirectToAction("ErrorView", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Landing");
        }


        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);


        /*[HttpPost]
       public async Task<IActionResult> Register(Register model, string submit)
       {
           *//*String client = "Client";
           String creator = "Creator";
           var roleResult1 = await _roleManager.CreateAsync(new IdentityRole(client));
           var roleResult2 = await _roleManager.CreateAsync(new IdentityRole(creator));*//*
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
           *//*var userData = await GetCurrentUserAsync();
           var userPekerjaan = userData.Pekerjaan;*//*
           return View(model);

       }*/
    }
}
