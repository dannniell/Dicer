using Microsoft.AspNetCore.Mvc;
using Dicer.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Dicer.Controllers.API
{
    [AllowAnonymous]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            this._paymentService = paymentService;
        }

        [Route("api/[Controller]/{userId}")]
        [HttpGet]
        public async Task<IActionResult> CheckSaldo(string userId)
        {
            var retVal = await _paymentService.CheckSaldo(userId);
            return Ok(retVal.Saldo);
        }

        [Route("api/[Controller]/{userId}/Withdrawal/{amount}")]
        [HttpPost]
        public async Task<IActionResult> WithdrawlSaldo(string userId, int amount)
        {
            var retVal = await _paymentService.WithdrawlSaldo(userId, amount);
            return Ok(retVal);
        }
    }
}
