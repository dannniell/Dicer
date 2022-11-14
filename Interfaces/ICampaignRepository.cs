using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface ICampaignRepository
    {
        public void UpsertCampaign(CampaignModel model);

        Task<bool> RegisterCampaign(string userId, int campaignId);
    }
}