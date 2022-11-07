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

        public CampaignController(ICampaignRepository campaignRepository,
                                        UserManager<ApplicationUser> userManager,
                                        IWebHostEnvironment webHostEnvironment)
        {
            this.campaignRepository = campaignRepository;
            this._userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCampaignViewModel model)
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



        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
