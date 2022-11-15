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
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager,
                                ApplicationDbContext context)
        {
            this._userManager = userManager;
            this._context = context;
        }

        #region Home Creator
        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpGet]
        public async Task<IActionResult> HomeCreator(int? pageNumber, string searchString, string genreString, int locationInt, int monthInt)
        {
            var user = await GetCurrentUserAsync();
            ViewData["SearchFilter"] = searchString;
            ViewData["genreString"] = genreString;
            ViewData["locationInt"] = locationInt;
            ViewData["monthInt"] = monthInt;
            var campaigns = from s in _context.Campaign
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                campaigns = campaigns.Where(s => s.CampaignName.Contains(searchString)
                                                || s.ContentType.Contains(searchString)
                                                || s.Genre.Contains(searchString));
            }
            if (!String.IsNullOrEmpty(genreString))
            {
                campaigns = campaigns.Where(s => s.Genre.Contains(genreString));
            }
            if (locationInt > 0)
            {
                campaigns = campaigns.Where(s => s.Provinsi == locationInt);
            }
            if (monthInt > 0)
            {
                campaigns = campaigns.Where(s => s.CreatedDate.Month == monthInt);
            }
            campaigns = campaigns.OrderByDescending(s => s.CreatedDate);
            int pageSize = 6;
            return View(await PaginatedList<Campaign>.CreateAsync(campaigns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        #endregion Home Creator


        #region Home Client
        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpGet]
        public async Task<IActionResult> HomeClient(int? pageNumber, string searchString)
        {
            var user = await GetCurrentUserAsync();
            ViewData["SearchFilter"] = searchString;
            var campaigns = from s in _context.Campaign
                            from b in _context.ClientCampaign
                            where b.UserId == user.Id
                                && s.CampaignId == b.CampaignId
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                campaigns = campaigns.Where(s => s.CampaignName.Contains(searchString)
                                                || s.ContentType.Contains(searchString)
                                                || s.Genre.Contains(searchString));
            }
            campaigns = campaigns.OrderByDescending(s => s.CreatedDate);
            int pageSize = 6;
            return View(await PaginatedList<Campaign>.CreateAsync(campaigns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        #endregion Home Client

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}