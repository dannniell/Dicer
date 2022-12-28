using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dicer.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Dicer.Models;

namespace Dicer.Controllers.API
{
    [AllowAnonymous]
    [ApiController]
    public class AcceptanceController : ControllerBase
    {
        private readonly IAcceptanceService _acceptanceService;

        public AcceptanceController(IAcceptanceService acceptanceService)
        {
            this._acceptanceService = acceptanceService;
        }

        [Route("api/[Controller]/{campaignId}")]
        [HttpGet]
        public async Task<IActionResult> GetRegistrant(int campaignId)
        {
            var retVals = await _acceptanceService.GetRegistrant(campaignId);
            return Ok(retVals);
        }
    }
}
