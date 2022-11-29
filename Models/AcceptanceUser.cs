using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    [Keyless]
    public class AcceptanceUser
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Kota { get; set; }
        public string Provinsi { get; set; }
        public int Followers { get; set; }
        public decimal ER { get; set; }
        public string LinkInstagram { get; set; }
        public bool IsAccepted { get; set; }
    }
}