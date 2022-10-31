using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dicer.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace Dicer.Controllers.API
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IProvinsiService provinsiService;
        private readonly IKotaService kotaService;

        public LocationController(IProvinsiService provinsiService, IKotaService kotaService)
        {
            this.provinsiService = provinsiService;
            this.kotaService = kotaService;
        }

        [Route("GetProvinsi")]
        [HttpGet]
        public async Task<IActionResult> GetAllProvinsiAsync()
        {
            var data = await provinsiService.GetAllProvinsi();
            var provinsi = data.Select(x => new SelectListItem() { Value = x.ProvinsiId.ToString(), Text = x.NamaProvinsi }).ToList();
            return Ok(provinsi);
            /*return new JsonResult(provinsi);*/
            /*return Json(provinsi);
            var data = await provinsiService.GetAllProvinsi();
            return Json(new { data });*/
        }

        [HttpGet("GetKota/{provinsiId}")]
        public async Task<IActionResult> GetAllKotaAsync(int provinsiId)
        {
            var data = await kotaService.GetAllKota(provinsiId);
            var kota = data.Select(x => new SelectListItem() { Value = x.KotaId.ToString(), Text = x.NamaKota }).ToList();
            return Ok(kota);
        }
    }
}
