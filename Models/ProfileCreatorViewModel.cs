using System.ComponentModel.DataAnnotations;

namespace Dicer.Models
{
    public class ProfileCreatorViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        [Required]
        public string Gender { get; set; }
        public IFormFile? ProfileImg { get; set; }

        [Required]
        public DateTime? DoB { get; set; }

        [Required]
        public int? Kota { get; set; }

        [Required]
        public int? Provinsi { get; set; }

        [Required]
        public string? UserNameIg { get; set; }
        public int? JumlahFollowers { get; set; }
        public int? ER { get; set; }
        /*public string? LinkInstagram { get; set; }*/
    }
}
