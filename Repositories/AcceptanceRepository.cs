using Dicer.Models;
using Dicer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Dicer.Repositories
{
    public class AcceptanceRepository : IAcceptanceService
    {
        private readonly ApplicationDbContext _context;

        public AcceptanceRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
       
        public async Task<List<AcceptanceUser>> GetRegistrant(int campaignId)
        {
            var param = new SqlParameter("@CampaignId", campaignId);
            var data = await _context.AcceptanceUser.FromSqlRaw(Constants.Constants.getAcceptance + " @CampaignId", param).ToListAsync();
            return data;
        }
    }
}
