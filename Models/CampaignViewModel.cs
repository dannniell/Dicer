using System.ComponentModel.DataAnnotations;

namespace Dicer.Models
{
    public class CampaignViewModel
    {
        public int? CampaignId { get; set; }

        [Required]
        [Display(Name = "Campaign Name")]
        public string CampaignName { get; set; }

        [Required]
        [Display(Name = "Content Type")]
        public string ContentType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Commission { get; set; }

        [Required]
        [Display(Name = "Task Description")]
        public string Task { get; set; }

        public IFormFile? CampaignImg { get; set; }

        public string? Gender { get; set; }

        public int? Kota { get; set; }

        public int? Provinsi { get; set; }

        [Range(1, Int32.MaxValue)]
        [Display(Name = "Minimal Followers")]
        public int? MinFollowers { get; set; }

        [Range(1, 100)]
        [Display(Name = "Minimal Age")]
        public int? MinAge { get; set; }

        [Range(1, 100)]
        [Display(Name = "Maximal Age")]
        public int? MaxAge { get; set; }
        public string Genre { get; set; }
    }
}
