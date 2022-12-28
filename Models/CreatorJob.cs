using System.ComponentModel.DataAnnotations;

namespace Dicer.Models
{
    public class CreatorJob
    {
        [Key]
        public string UserId { get; set; }
        public int CampaignId { get; set; }

        public bool IsOnProgress { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsDeclined { get; set; }

        public bool IsDone { get; set; }

        public string? FinalDraft { get; set; }

        public string? PostLink { get; set; }

        public bool IsTaskDone { get; set; }

        public bool IsInsightDone { get; set; }

        public string? Insight { get; set; }
    }
}
