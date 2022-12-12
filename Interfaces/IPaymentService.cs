using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface IPaymentService
    {
        public Task<SaldoCreator> CheckSaldo(string userId);
        public Task<bool> WithdrawlSaldo(string userId, int amount);
    }
}
