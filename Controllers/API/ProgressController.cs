using Microsoft.AspNetCore.Mvc;
using Dicer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Dicer.Models;

namespace Dicer.Controllers.API
{
    [AllowAnonymous]
    [ApiController]
    public class ProgressController : ControllerBase
    {
        private readonly IProgressService _progressService;

        public ProgressController(IProgressService progressService)
        {
            this._progressService = progressService;
        }

        [Route("api/[Controller]/{campaignId}")]
        [HttpGet]
        public async Task<IActionResult> GetProgress(int campaignId)
        {
            var retVals = await _progressService.GetProgress(campaignId);
            return Ok(retVals);
        }

        [Route("api/[Controller]/{campaignId}")]
        [HttpPost]
        public async Task<IActionResult> AcceptParticipant(int campaignId, AcceptParticipant model)
        {
            var retVals = await _progressService.AcceptParticipant(campaignId, model);
            return Ok(retVals);
        }
    }
}