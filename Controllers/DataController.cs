using Dicer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using Dicer.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Dicer.Controllers
{
    [AllowAnonymous]
    public class DataController : Controller
    {
        private readonly IKotaService kotaService;
        private readonly IProvinsiService provinsiService;

        public DataController(IKotaService kotaService,
                                IProvinsiService provinsiService)
        {
            this.kotaService = kotaService;
            this.provinsiService = provinsiService;
        }
        [HttpGet("api/GetKota/{provinsiId}")]
        public async Task<IActionResult> GetAllKotaAsync(int provinsiId)
        {
            var data = await kotaService.GetAllKota(provinsiId);
            var kota = data.Select(x => new SelectListItem() { Value = x.KotaId.ToString(), Text = x.NamaKota }).ToList();
            return Json(kota);
        }

        [HttpGet("api/GetProvinsi")]
        public async Task<JsonResult> GetAllProvinsiAsync()
        {
            var data = await provinsiService.GetAllProvinsi();
            var provinsi = data.Select(x => new SelectListItem() { Value = x.ProvinsiId.ToString(), Text = x.NamaProvinsi }).ToList();
            return Json(provinsi);
            /*var data = await provinsiService.GetAllProvinsi();
            return Json(new {data});*/
        }
    }
}
