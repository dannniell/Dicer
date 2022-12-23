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
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IProvinsiService provinsiService;
        private readonly IEmailService emailService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IApiIgService apiIgService;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<IdentityRole> roleManager,
                                IProvinsiService provinsiService,
                                IEmailService emailService,
                                IWebHostEnvironment webHostEnvironment,
                                IApiIgService apiIgService,
                                ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            this.provinsiService = provinsiService;
            this.emailService = emailService;
            this.webHostEnvironment = webHostEnvironment;
            this.apiIgService = apiIgService;
            this._context = context;
        }

        #region Register Client
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var defaultImg = Constants.Constants.DefaultProfileImg;
                var user = new ApplicationUser { Email = model.Email, UserName = model.Email, Name = model.Name, ProfileImg = defaultImg};
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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult RegisterCreator()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterCreator(RegisterCreator model)
        {
            if (ModelState.IsValid)
            {
                var defaultImg = Constants.Constants.DefaultProfileImg;
                var dataIg = await apiIgService.GetDataIg();
                var userDetailIg = dataIg.FirstOrDefault();

                var user = new ApplicationUser { Email = model.Email, UserName = model.Email, Name = model.Name,Gender = model.Gender, DoB = model.DoB, 
                                                UserNameIg = model.UsernameIg, Provinsi = model.Provinsi, Kota = model.Kota, ProfileImg = defaultImg,
                                                JumlahFollowers = userDetailIg.JumlahFollowers, ER = userDetailIg.ER};
                
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var paymentParam = new Payment
                    {
                        UserId = user.Id,
                        Saldo = 0
                    };

                    _context.Payment.Add(paymentParam);
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

        #region Profile Creator
        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpGet]
        public async Task<IActionResult> ProfileCreator()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToAction("ErrorView", "Account");
            }
            var model = new ProfileCreatorViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Gender = user.Gender,
                DoB = user.DoB,
                Kota = user.Kota,
                Provinsi = user.Provinsi,
                UserNameIg = user.UserNameIg,
                JumlahFollowers = user.JumlahFollowers,
                ER = user.ER
            };
            return View(model);
        }

        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpPost]
        public async Task<IActionResult> ProfileCreator(ProfileCreatorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                string? uniqueFileName = null;
                if (user == null)
                {
                    return RedirectToAction("ErrorView", "Account");
                }
                else
                {
                    if (model.ProfileImg != null)
                    {
                        var extension = Path.GetExtension(model.ProfileImg.FileName);
                        if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                        {
                            string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "Img", "Profile");
                            uniqueFileName = user.Id + extension;
                            string filePath = Path.Combine(uploadFolder, uniqueFileName);

                            //delete when exist (replace)
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }

                            FileStream fs = new FileStream(filePath, FileMode.Create);
                            model.ProfileImg.CopyTo(fs);
                            fs.Close();
                            user.ProfileImg = uniqueFileName;
                        }
                    }
                    user.Name = model.Name;
                    user.Gender = model.Gender;
                    user.DoB = model.DoB;
                    user.Kota = model.Kota;
                    user.Provinsi = model.Provinsi;
                    user.UserNameIg = model.UserNameIg;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ProfileCreator", "Account");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        #endregion Profile Creator

        #region Profile Client

        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpGet]
        public async Task<IActionResult> ProfileClient()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return RedirectToAction("ErrorView", "Account");
            }
            var model = new ProfileClientViewModel
            {
                Name = user.Name,
                Email = user.Email
            };
            return View(model);
        }

        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpPost]
        public async Task<IActionResult> ProfileClient(ProfileClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                string? uniqueFileName = null;
                if (user == null)
                {
                    return RedirectToAction("ErrorView", "Account");
                }
                else
                {
                    if (model.ProfileImg != null)
                    {
                        var extension = Path.GetExtension(model.ProfileImg.FileName);
                        if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                        {
                            string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "Img", "Profile");
                            uniqueFileName = user.Id + extension;
                            string filePath = Path.Combine(uploadFolder, uniqueFileName);

                            //delete when exist (replace)
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }

                            FileStream fs = new FileStream(filePath, FileMode.Create);
                            model.ProfileImg.CopyTo(fs);
                            fs.Close();
                            user.ProfileImg = uniqueFileName;
                        }
                    }

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ProfileClient", "Account");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        #endregion Profile Client
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("ErrorView", "Account");
            }
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    // reset the user password
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ResetPasswordSuccess", "Account");
                    }
                    // Display validation errors. For example, password reset token already
                    // used to change the password or password complexity rules not met
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                // To avoid account enumeration and brute force attacks, don't
                // reveal that the user does not exist
                return RedirectToAction("ResetPasswordSuccess", "Account");
            }
            // Display validation errors if model state is not valid
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ResetPasswordSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                // If the user is found AND Email is confirmed
                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    // Generate the reset password token
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    // Build the password reset link
                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);

                    string body = String.Format("<th><a href={0}>Klik Disini Untuk Reset Password</a></th>", passwordResetLink);
                    emailService.SendEmail(model.Email, "Dicer Reset Password", body);
                    return RedirectToAction("ForgotPasswordSuccess", "Account");
                }
                return RedirectToAction("ForgotPasswordSuccess", "Account");
            }

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPasswordSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult RegistrationSuccess()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ErrorView()
        {
            return View();
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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
