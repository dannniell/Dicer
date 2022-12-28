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
        private readonly ApplicationDbContext _context;

        public ChatController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                ApplicationDbContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._context = context;
        }

        //for client
        [HttpGet]
        public async Task<IActionResult> Index(int campaignId, string? userId)
        {
            ViewData["campaignId"] = campaignId;
            var user = await GetCurrentUserAsync();
            if(userId == null)
            {
                var campaignData = from a in _context.ClientCampaign
                                   where a.CampaignId == campaignId
                                   select a;
                userId = campaignData.FirstOrDefault().UserId;
            }
            var target = await _userManager.FindByIdAsync(userId);
            ViewData["currentEmail"] = user.NormalizedEmail;
            if (User.IsInRole("Client"))
            {
                ViewData["clientMail"] = user.NormalizedEmail;
                ViewData["creatorMail"] = target.NormalizedEmail;
            }
            else
            {
                ViewData["clientMail"] = target.NormalizedEmail;
                ViewData["creatorMail"] = user.NormalizedEmail;
            }
            ViewData["name"] = target.Name;
            return View();
        }

        //for creator
        [HttpGet]
        /*public async Task<IActionResult> Index(int cId)
        {
            ViewData["campaignId"] = cId;
            var user = await GetCurrentUserAsync();
            var campaignData = from a in _context.ClientCampaign
                               where a.CampaignId == cId
                               select a;

            var target = await _userManager.FindByIdAsync(campaignData.FirstOrDefault().UserId);
            ViewData["currentEmail"] = user.NormalizedEmail;
            if (User.IsInRole("Client"))
            {
                ViewData["clientMail"] = user.NormalizedEmail;
                ViewData["creatorMail"] = target.NormalizedEmail;
            }
            else
            {
                ViewData["clientMail"] = target.NormalizedEmail;
                ViewData["creatorMail"] = user.NormalizedEmail;
            }
            ViewData["name"] = target.Name;
            return View();
        }*/

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
