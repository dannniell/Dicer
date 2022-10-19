using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    [Table("dbo.ApiIg")]
    public class ApiIGModel
    {
        [Key]
        public int IgId { get; set; }
        public int JumlahFollowers { get; set; }
        public decimal ER { get; set; }
    }
}
