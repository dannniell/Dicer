using System.ComponentModel.DataAnnotations;

namespace Dicer.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
