using System.ComponentModel.DataAnnotations;

namespace Dicer.Models
{
    public class ProfileCreatorViewModel
    {
        [Required]
        public string Name { get; set; }

        public string? Email { get; set; }

        [Required]
        public string Gender { get; set; }
        public IFormFile? ProfileImg { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime? DoB { get; set; }

        [Required]
        public int? Kota { get; set; }

        [Required]
        public int? Provinsi { get; set; }

        [Required]
        [Display(Name = "Username Instagram")]
        public string? UserNameIg { get; set; }

        [Display(Name = "Followers")]
        public int? JumlahFollowers { get; set; }

        [Display(Name = "Engagement Rate")]
        public decimal? ER { get; set; }
    }
}
