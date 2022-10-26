namespace Dicer.Models
{
    public class CampaignModel
    {
        public int? CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        public int Commission { get; set; }
        public string Task { get; set; }

        public string? CampaignImg { get; set; }

        public string? Gender { get; set; }

        public int? Kota { get; set; }

        public int? Provinsi { get; set; }
        public int? MinFollowers { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? UserId { get; set; }
    }
}
