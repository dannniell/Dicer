using Dicer.Models;

namespace Dicer.Interfaces
{
    public interface ICampaignRepository
    {
/*        public void UpsertCampaign(string campaignId, string campaignName, string contentType, string description, string commission,
                                    string task, string gender, int provinsi, int kota, string campaignImg, int minFollowers, int minAge, int maxAge);*/
        public void UpsertCampaign(CampaignModel model);
    }
}