using Microsoft.AspNetCore.Mvc;
using Dicer.Models;
using Microsoft.AspNetCore.Authorization;

namespace Dicer.Controllers
{
    public class CreateCampaign : Controller
    {
        [Authorize(Roles = Constants.Constants.roleIdClient)]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = Constants.Constants.roleIdClient)]
        [HttpPost]
        public async Task<IActionResult> Index(CreateCampaignViewModel model)
        {
            return View(model);
        }
    }
}
