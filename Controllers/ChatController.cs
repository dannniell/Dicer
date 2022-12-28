using Dicer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dicer.Interfaces;

namespace Dicer.Controllers
{
    [AllowAnonymous]
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ChatController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        //for client
        [HttpGet]
        public async Task<IActionResult> Index(int campaignId, string userId)
        {
            ViewData["campaignId"] = campaignId;
            var client = await GetCurrentUserAsync();
            ViewData["currentEmail"] = client.NormalizedEmail;
            ViewData["clientMail"] = client.NormalizedEmail;
            var creator = await _userManager.FindByIdAsync(userId);
            ViewData["creatorMail"] = creator.NormalizedEmail;
            ViewData["name"] = creator.Name;
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
