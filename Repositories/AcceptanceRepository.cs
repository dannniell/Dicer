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

        public async Task<bool> AcceptParticipant(int campaignId, AcceptParticipant model)
        {
            var campaign = new SqlParameter("@CampaignId", campaignId);
            var commision = new SqlParameter("@Commision", model.Paid);
            foreach (var item in model.users)
            {
                var user = new SqlParameter("@UserId", item.userId);
                try
                {
                    var data = await _context.Database.ExecuteSqlRawAsync(Constants.Constants.acceptParticipant + " @UserId, @CampaignId", user, campaign);
                    await _context.Database.ExecuteSqlRawAsync(Constants.Constants.pay + " @UserId, @Commision", user, commision);
                }
                catch (Exception ex)
                {
                    var a = ex;
                }
            }
            await _context.Database.ExecuteSqlRawAsync(Constants.Constants.declineParticipant + " @CampaignId", campaign);
            await _context.Database.ExecuteSqlRawAsync(Constants.Constants.paidCampaign + " @CampaignId", campaign);

            return true;
        }
    }
}
