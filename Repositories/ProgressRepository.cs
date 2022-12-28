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
       
        public async Task<List<ProgressCampaign>> GetProgress(int campaignId)
        {
            var param = new SqlParameter("@CampaignId", campaignId);
            var data = await _context.ProgressCampaigns.FromSqlRaw(Constants.Constants.getProgress + " @CampaignId", param).ToListAsync();
            return data;
        }

        public async Task<bool> TaskDoneParticipant(int campaignId, string userId)
        {
            var campaign = new SqlParameter("@CampaignId", campaignId);
            var user = new SqlParameter("@UserId", userId);
            await _context.Database.ExecuteSqlRawAsync(Constants.Constants.taskDone + " @CampaignId, @UserId", campaign, user);

            return true;
        }

        public async Task<List<ClientWithdrawl>> GetWithdrawlAmmount(int campaignId)
        {
            var param = new SqlParameter("@CampaignId", campaignId);
            var data = await _context.ClientWithdrawls.FromSqlRaw(Constants.Constants.clientWithdrawl + " @CampaignId", param).ToListAsync();
            return data;
        }

        public async Task<bool> Completed(int campaignId)
        {
            var campaign = new SqlParameter("@CampaignId", campaignId);
            await _context.Database.ExecuteSqlRawAsync(Constants.Constants.closeCampaign + " @CampaignId", campaign);

            return true;
        }
    }
}
