using System.ComponentModel.DataAnnotations;

namespace Dicer.Models
{
    public class CreateCampaignViewModel
    {
        [Required]
        public string CampaignName { get; set; }

        [Required]
        public int ContentType { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Commission { get; set; }

        [Required]
        public string Task { get; set; }

        public IFormFile? CampaignImg { get; set; }

        public string? Gender { get; set; }

        public int? Kota { get; set; }

        public int? Provinsi { get; set; }

        [Display(Name = "Minimal Followers")]
        public int? MinFollowers { get; set; }

        [Display(Name = "Minimal Age")]
        public int? MinAge { get; set; }

        [Display(Name = "Maximal Age")]
        public int? MaxAge { get; set; }
    }
}
