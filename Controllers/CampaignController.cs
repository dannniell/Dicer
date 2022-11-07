using Microsoft.AspNetCore.Mvc;
using Dicer.Models;
using Microsoft.AspNetCore.Authorization;
using Dicer.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Dicer.Controllers
{
    public class CampaignController : Controller
    {
        private readonly ICampaignRepository campaignRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ApplicationDbContext _context;

        public CampaignController(ICampaignRepository campaignRepository,
                                        UserManager<ApplicationUser> userManager,
                                        IWebHostEnvironment webHostEnvironment,
                                        ApplicationDbContext context)
        {
            this.campaignRepository = campaignRepository;
            this._userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this._context = context;
        }

        #region Create
        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpPost]
        public async Task<IActionResult> Create(CampaignViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.MinAge > model.MaxAge)
                {
                    return View(model);
                }

                var user = await GetCurrentUserAsync();
                int? id = null;

                if (user == null)
                {
                    return RedirectToAction("ErrorView", "Account");
                }
                else
                {
                    string? uniqueFileName = null;
                    if (model.CampaignImg != null)
                    {
                        var extension = Path.GetExtension(model.CampaignImg.FileName);
                        if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                        {
                            string uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "Img", "Campaign");
                            uniqueFileName = Guid.NewGuid() + extension;
                            string filePath = Path.Combine(uploadFolder, uniqueFileName);

                            FileStream fs = new FileStream(filePath, FileMode.Create);
                            model.CampaignImg.CopyTo(fs);
                            fs.Close();
                        }
                    }
                    else
                    {
                        uniqueFileName = Constants.Constants.DefaultProfileImg;
                    }

                    CampaignModel newCampaign = new CampaignModel
                    {
                        CampaignId = id,
                        CampaignName = model.CampaignName,
                        ContentType = model.ContentType,
                        Description = model.Description,
                        Commission = model.Commission,
                        Task = model.Task,
                        CampaignImg = uniqueFileName,
                        Gender = model.Gender,
                        Kota = model.Kota,
                        Provinsi = model.Provinsi,
                        MinFollowers = model.MinFollowers,
                        MinAge = model.MinAge,
                        MaxAge = model.MaxAge,
                        UserId = user.Id,
                        UserName = user.Name,
                        Genre = model.Genre
                    };

                    campaignRepository.UpsertCampaign(newCampaign);

                    return RedirectToAction("HomeClient", "Home");
                }
            }
            return View(model);
        }
        #endregion

        #region Edit
        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await GetCurrentUserAsync();
            var check = from b in _context.ClientCampaign
                            where b.UserId == user.Id
                                && b.CampaignId == id
                            select b;

            if(check.Count()<1 || user == null)
            {
                return RedirectToAction("ErrorView", "Account");
            }

            var campaign = _context.Campaign
                            .Where(s => s.CampaignId == id)
                            .FirstOrDefault();

            ViewData["Img"] = campaign.CampaignImg;
            var model = new CampaignViewModel
            {
                CampaignId = id,
                CampaignName = campaign.CampaignName,
                ContentType = campaign.ContentType,
                Description = campaign.Description,
                Commission = campaign.Commission,
                Task = campaign.Task,
                Gender = campaign.Gender,
                Kota = campaign.Kota,
                Provinsi = campaign.Provinsi,
                MinFollowers = campaign.MinFollowers,
                MinAge = campaign.MinAge,
                MaxAge = campaign.MaxAge,
                Genre = campaign.Genre
            };
            return View(model);
        }

        #endregion

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
