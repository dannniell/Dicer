using Microsoft.AspNetCore.Identity;

namespace Dicer.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? ProfileImg { get; set; }
        public DateTime? DoB { get; set; }
        public string? Kota { get; set; }
        public string? Provinsi { get; set; }
        public string? Pekerjaan { get; set; }
        public string? Minat { get; set; }
    }
}