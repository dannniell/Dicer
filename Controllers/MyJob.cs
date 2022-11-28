using Dicer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Dicer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Dicer.Models;
using Microsoft.EntityFrameworkCore;

namespace Dicer.Controllers
{
    public class MyJob : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public MyJob(UserManager<ApplicationUser> userManager,
                                ApplicationDbContext context)
        {
            this._userManager = userManager;
            this._context = context;
        }

        #region OnProgress
        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpGet]
        public async Task<IActionResult> OnProgress(int? pageNumber)
        {
            var user = await GetCurrentUserAsync();
            var campaigns = from campaign in _context.Campaign
                    from d in _context.CreatorJob
                    where d.UserId == user.Id && d.CampaignId == campaign.CampaignId && d.IsOnProgress
                    select campaign;
            
            int pageSize = 6;
            return View(await PaginatedList<Campaign>.CreateAsync(campaigns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        #endregion 

        #region Accepted
        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpGet]
        public async Task<IActionResult> Accepted(int? pageNumber)
        {
            var user = await GetCurrentUserAsync();
            var campaigns = from campaign in _context.Campaign
                            from d in _context.CreatorJob
                            where d.UserId == user.Id && d.CampaignId == campaign.CampaignId && d.IsAccepted
                            select campaign;

            int pageSize = 6;
            return View(await PaginatedList<Campaign>.CreateAsync(campaigns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        #endregion 

        #region Declined
        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpGet]
        public async Task<IActionResult> Declined(int? pageNumber)
        {
            var user = await GetCurrentUserAsync();
            var campaigns = from campaign in _context.Campaign
                            from d in _context.CreatorJob
                            where d.UserId == user.Id && d.CampaignId == campaign.CampaignId && d.IsDeclined
                            select campaign;

            int pageSize = 6;
            return View(await PaginatedList<Campaign>.CreateAsync(campaigns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        #endregion 

        #region Done
        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpGet]
        public async Task<IActionResult> Done(int? pageNumber)
        {
            var user = await GetCurrentUserAsync();
            var campaigns = from campaign in _context.Campaign
                            from d in _context.CreatorJob
                            where d.UserId == user.Id && d.CampaignId == campaign.CampaignId && d.IsDone
                            select campaign;

            int pageSize = 6;
            return View(await PaginatedList<Campaign>.CreateAsync(campaigns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        #endregion 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}