using Dicer.Models;
using Dicer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace Dicer.Repositories
{
    public class ProgressRepository : IProgressService
    {
        private readonly ApplicationDbContext _context;

        public ProgressRepository(ApplicationDbContext context)
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
            foreach (var item in model.users)
            {
                var user = new SqlParameter("@UserId", item.userId);
                try
                {
                    var data = await _context.Database.ExecuteSqlRawAsync(Constants.Constants.acceptParticipant + " @UserId, @CampaignId", user, campaign);
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

        public async Task<bool> Completed(int campaignId)
        {
            var campaign = new SqlParameter("@CampaignId", campaignId);
            await _context.Database.ExecuteSqlRawAsync(Constants.Constants.pay + " @CampaignId", campaign);
            return true;
        }
    }
}
