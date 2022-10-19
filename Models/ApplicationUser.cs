using Microsoft.AspNetCore.Identity;

namespace Dicer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? ProfileImg { get; set; }
        public DateTime? DoB { get; set; }
        public int? Kota { get; set; }
        public int? Provinsi { get; set; }
        public string? Pekerjaan { get; set; }
        public string? Minat { get; set; }
        public string? UserNameIg { get; set; }
        public int? JumlahFollowers { get; set; }
        public decimal? ER { get; set; }
        public string? LinkInstagram { get; set; }
    }
}