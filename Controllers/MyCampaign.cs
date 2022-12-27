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
    public class MyCampaign : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public MyCampaign(UserManager<ApplicationUser> userManager,
                                ApplicationDbContext context)
        {
            this._userManager = userManager;
            this._context = context;
        }

        #region Registered
        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpGet]
        public async Task<IActionResult> Registered(int? pageNumber)
        {
            var user = await GetCurrentUserAsync();
            var campaigns = from campaign in _context.Campaign
                    from a in _context.ClientCampaign
                    where a.UserId == user.Id && a.CampaignId == campaign.CampaignId 
                        && campaign.IsDone == false
                    select campaign;
            
            int pageSize = 6;
            return View(await PaginatedList<Campaign>.CreateAsync(campaigns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        #endregion 

        #region Done
        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpGet]
        public async Task<IActionResult> Done(int? pageNumber)
        {
            var user = await GetCurrentUserAsync();
            var campaigns = from campaign in _context.Campaign
                            from a in _context.ClientCampaign
                            where a.UserId == user.Id && a.CampaignId == campaign.CampaignId
                                && campaign.IsDone == true
                            select campaign;

            int pageSize = 6;
            return View(await PaginatedList<Campaign>.CreateAsync(campaigns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        #endregion

        #region Accept
        public async Task<IActionResult> Accept(int id)
        {
            var check = from campaign in _context.Campaign
                        where campaign.CampaignId == id
                        select campaign;
            if(check.FirstOrDefault() == null)
            {
                return RedirectToAction("ErrorView", "Account");
            }
            ViewData["campaignId"] = id;

            var campaignData = check.FirstOrDefault();
            var model = new CampaignViewModel
            {
                CampaignId = id,
                CampaignName = campaignData.CampaignName,
                ClientName = campaignData.ClientName,
                ContentType = campaignData.ContentType,
                Description = campaignData.Description,
                Commission = campaignData.Commission,
                Task = campaignData.Task,
                Gender = campaignData.Gender,
                MinFollowers = campaignData.MinFollowers,
                MinAge = campaignData.MinAge,
                MaxAge = campaignData.MaxAge,
                Genre = campaignData.Genre
            };

            return View(model);
        }
        #endregion

        #region ProgressCampaign
        public async Task<IActionResult> ProgressCampaign(int id)
        {
            var check = from campaign in _context.Campaign
                        where campaign.CampaignId == id
                        select campaign;
            if (check.FirstOrDefault() == null)
            {
                return RedirectToAction("ErrorView", "Account");
            }
            ViewData["campaignId"] = id;

            var campaignData = check.FirstOrDefault();
            var model = new CampaignViewModel
            {
                CampaignId = id,
                CampaignName = campaignData.CampaignName,
                ClientName = campaignData.ClientName,
                ContentType = campaignData.ContentType,
                Description = campaignData.Description,
                Commission = campaignData.Commission,
                Task = campaignData.Task,
                Gender = campaignData.Gender,
                MinFollowers = campaignData.MinFollowers,
                MinAge = campaignData.MinAge,
                MaxAge = campaignData.MaxAge,
                Genre = campaignData.Genre
            };

            return View(model);
        }
        #endregion

        public async Task<IActionResult> Completed(int id)
        {
            var check = from campaign in _context.Campaign
                        where campaign.CampaignId == id
                        select campaign;
            if (check.FirstOrDefault() == null)
            {
                return RedirectToAction("ErrorView", "Account");
            }
            ViewData["campaignId"] = id;
            ViewData["price"] = check.FirstOrDefault().Commission;

            return RedirectToAction("Done", "MyCampaign");
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}