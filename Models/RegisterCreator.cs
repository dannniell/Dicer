using System.ComponentModel.DataAnnotations;

namespace Dicer.Models
{
    public class RegisterCreator
    {
        [Required]
        [StringLength(128, ErrorMessage ="Name max character is 128", MinimumLength = 1)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username Instagram")]
        public string UsernameIg { get; set; }

        [Required]
        [Display(Name = "Kota")]
        public string Kota { get; set; }

        [Required]
        [Display(Name = "Provinsi")]
        public string Provinsi { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
