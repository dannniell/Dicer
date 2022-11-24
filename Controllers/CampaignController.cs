﻿using Microsoft.AspNetCore.Mvc;
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

        [Authorize(Roles = Constants.Constants.roleNameClient)]
        [HttpPost]
        public async Task<IActionResult> Edit(CampaignViewModel model)
        {
            if (ModelState.IsValid)
            {
                //ViewData["Id"] = searchString;
                if (model.MinAge > model.MaxAge)
                {
                    return View(model);
                }

                var user = await GetCurrentUserAsync();

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

                    CampaignModel newCampaign = new CampaignModel
                    {
                        CampaignId = model.CampaignId,
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

                    return RedirectToAction("Detail", new { id = model.CampaignId });
                }
            }
            return View(model);
        }

        #endregion

        #region Detail
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var user = await GetCurrentUserAsync();
            var roleClient = await _userManager.IsInRoleAsync(user, Constants.Constants.roleNameClient);
            ViewData["isApply"] = "false";

            if (roleClient)
            {
                var check = from b in _context.ClientCampaign
                            where b.UserId == user.Id
                                && b.CampaignId == id
                            select b;

                if (check.Count() < 1 || user == null)
                {
                    return RedirectToAction("ErrorView", "Account");
                }
            }
            else
            {
                var check = from b in _context.CreatorJob
                           where b.UserId == user.Id
                             && b.CampaignId == id
                           select b;

                if (check.Count() > 0)
                {
                    ViewData["isApply"] = "true";
                }
            }

            var campaign = _context.Campaign
                            .Where(s => s.CampaignId == id)
                            .FirstOrDefault();
            var provinsi = _context.Provinsi
                            .Where(s => s.ProvinsiId == campaign.Provinsi)
                            .FirstOrDefault();
            var kota = _context.Kota
                        .Where(s => s.KotaId == campaign.Kota)
                        .FirstOrDefault();

            ViewData["Img"] = campaign.CampaignImg;
            ViewData["Provinsi"] = provinsi.NamaProvinsi;
            ViewData["Kota"] = kota.NamaKota;
            var model = new CampaignViewModel
            {
                CampaignId = id,
                CampaignName = campaign.CampaignName,
                ClientName = campaign.ClientName,
                ContentType = campaign.ContentType,
                Description = campaign.Description,
                Commission = campaign.Commission,
                Task = campaign.Task,
                Gender = campaign.Gender,
                MinFollowers = campaign.MinFollowers,
                MinAge = campaign.MinAge,
                MaxAge = campaign.MaxAge,
                Genre = campaign.Genre
            };
            return View(model);
        }
        #endregion

        #region Register
        [Authorize(Roles = Constants.Constants.roleNameCreator)]
        [HttpPost]
        public async Task<IActionResult> Register(int campaignId)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();

                if (user == null)
                {
                    return RedirectToAction("ErrorView", "Account");
                }
                var campaign = _context.Campaign
                            .Where(s => s.CampaignId == campaignId)
                            .FirstOrDefault();

                var valid = true;

                if (campaign == null)
                {
                    return RedirectToAction("ErrorView", "Account");
                }
                else
                {
                    //check followers
                    if(campaign.MinFollowers != null)
                    {
                        if(campaign.MinFollowers > user.JumlahFollowers)
                        {
                            valid = false;
                        }
                    }

                    //check age
                    if(campaign.MinAge != null)
                    {
                        var today = DateTime.Today;

                        // Calculate the age.
                        var age = today.Year - user.DoB.Value.Year;
                        if (campaign.MinAge > age)
                        {
                            valid = false;
                        }
                    }
                    if (campaign.MaxAge != null)
                    {
                        var today = DateTime.Today;

                        // Calculate the age.
                        var age = today.Year - user.DoB.Value.Year;
                        if (campaign.MaxAge < age)
                        {
                            valid = false;
                        }
                    }

                    //gender
                    if (campaign.Gender != null)
                    {
                        if(campaign.Gender != user.Gender)
                        {
                            valid = false;
                        }
                    }
                }

                if (!valid)
                {
                    return RedirectToAction("ErrorView", "Account");
                }
                else
                {
                    var userId = user.Id;
                    var result = await campaignRepository.RegisterCampaign(userId, campaignId);

                    if (!result)
                    {
                        return RedirectToAction("ErrorView", "Account");
                    }
                    return RedirectToAction("OnProgress", "MyJob");
                }
            }
            return RedirectToAction("Detail", new { id = campaignId });
        }
        #endregion

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
