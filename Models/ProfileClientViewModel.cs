using System.ComponentModel.DataAnnotations;

namespace Dicer.Models
{
    public class ProfileClientViewModel
    {
        [Required]
        public string Name { get; set; }

        public string? Email { get; set; }

        public IFormFile? ProfileImg { get; set; }
    }
}