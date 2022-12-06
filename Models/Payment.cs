using System.ComponentModel.DataAnnotations;

namespace Dicer.Models
{
    public class Payment
    {
        [Key]
        public string UserId { get; set; }
        public long Saldo { get; set; }
    }
}
