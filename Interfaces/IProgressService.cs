using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface IProgressService
    {
        public Task<List<ProgressCampaign>> GetProgress(int campaignId);
        public Task<bool> TaskDoneParticipant(int campaignId, string userId);

        public Task<List<ClientWithdrawl>> GetWithdrawlAmmount(int campaignId);

        public Task<bool> Completed(int campaignId);
    }
}
