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

        [Route("api/[Controller]/{campaignId}/TaskDone/{userId}")]
        [HttpPost]
        public async Task<IActionResult> TaskDoneParticipant(int campaignId, string userId)
        {
            var retVals = await _progressService.TaskDoneParticipant(campaignId, userId);
            return Ok(retVals);
        }

        [Route("api/[Controller]/WithdrawlCheck/{campaignId}")]
        [HttpGet]
        public async Task<IActionResult> GetWithdrawlAmmount(int campaignId)
        {
            var retVals = await _progressService.GetWithdrawlAmmount(campaignId);
            return Ok(retVals);
        }

        [Route("api/[Controller]/{campaignId}/Completed")]
        [HttpPost]
        public async Task<IActionResult> Completed(int campaignId)
        {
            var retVals = await _progressService.Completed(campaignId);
            return Ok(retVals);
        }
    }
}