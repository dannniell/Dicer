using Microsoft.EntityFrameworkCore;

namespace Dicer.Models
{
    [Keyless]
    public class ProgressCampaign
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public int Followers { get; set; }
        public decimal ER { get; set; }
        public string LinkInstagram { get; set; }
        public string? FinalDraft { get; set; }
        public string? PostLink { get; set; }
        public string? Insight { get; set; }
        public bool IsTaskDone { get; set; }
    }
}