using Dicer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Dicer.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Dicer.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }

        #region Home Creator
        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpGet]
        public IActionResult HomeCreator()
        {
            return View();
        }

        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpPost]
        public async Task<IActionResult> HomeCreator(HomeModel model)
        {
            return View(model);
        }
        #endregion Home Creator


        #region Home Client
        /*[Authorize(Roles = Constants.Constants.roleIdClient)]*/
        [AllowAnonymous]
        [HttpGet]
        public IActionResult HomeClient()
        {
            return View();
        }

        [Authorize(Roles = Constants.Constants.roleIdClient)]
        [HttpPost]
        public async Task<IActionResult> HomeClient(HomeModel model)
        {
            return View(model);
        }
        #endregion Home Client

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}