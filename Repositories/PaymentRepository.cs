using Dicer.Models;
using Dicer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Dicer.Repositories
{
    public class PaymentRepository : IPaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
       
        public async Task<SaldoCreator> CheckSaldo(string userId)
        {
            var ParamUserId = new SqlParameter("@UserId", userId);
            var data = await _context.Saldo.FromSqlRaw(Constants.Constants.getSaldo + " @UserId", ParamUserId).ToListAsync();
            SaldoCreator retVal;
            retVal = null;
            foreach (var item in data)
            {
                retVal = item;
            }
            return retVal;
        }

        public async Task<bool> WithdrawlSaldo(string userId, int amount)
        {
            var ParamUserId = new SqlParameter("@UserId", userId);
            var ParamAmount = new SqlParameter("@Amount", amount);
            await _context.Database.ExecuteSqlRawAsync(Constants.Constants.withdrawlSaldo + " @UserId, @Amount", ParamUserId, ParamAmount);
            return true;
        }
    }
}
