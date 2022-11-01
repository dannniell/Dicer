using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    public class Campaign
    {
        [Key]
        public int? CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        public int Commission { get; set; }
        public string Task { get; set; }
        public string? Gender { get; set; }
        public int? Provinsi { get; set; }
        public int? Kota { get; set; }
        public string? CampaignImg { get; set; }
        public int? MinFollowers { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public bool? IsPaid { get; set; }
        public bool? IsDone { get; set; }
        public string ClientName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Genre { get; set; }
    }
}
