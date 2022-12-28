using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface IProgressService
    {
        public Task<List<ProgressCampaign>> GetProgress(int campaignId);
        public Task<bool> AcceptParticipant(int campaignId, AcceptParticipant model);

        public Task<bool> Completed(int campaignId);
    }
}
