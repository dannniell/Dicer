using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    public class Provinsi
    {
        [Key]
        public int ProvinsiId { get; set; }
        public string NamaProvinsi { get; set; }
    }
}
