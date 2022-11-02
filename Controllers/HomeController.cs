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
        public async Task<IActionResult> HomeCreator(int? pageNumber)
        {
            var a = from s in _context.Campaign
                    select s;

            a = a.OrderBy(s => s.Gender);

            var c = await a.AsNoTracking().ToListAsync();
            string d = null;

            int pageSize = 1;
            return View(await PaginatedList<Campaign>.CreateAsync(a.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpPost]
        public async Task<IActionResult> HomeCreator(HomeModel model)
        {
            return View(model);
        }
        #endregion Home Creator


        #region Home Client
        /*[Authorize(Roles = Constants.Constants.roleNameClient)]*/
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> HomeClient(int? pageNumber, string searchString)
        {
            ViewData["SearchFilter"] = searchString;
            var campaigns = from s in _context.Campaign
                    select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                campaigns = campaigns.Where(s => s.CampaignName.Contains(searchString));
            }
            campaigns = campaigns.OrderByDescending(s => s.CreatedDate);
            int pageSize = 3;
            return View(await PaginatedList<Campaign>.CreateAsync(campaigns.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = Constants.Constants.roleNameClient)]
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