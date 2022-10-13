﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    [Table("dbo.Kota")]
    public class Kota
    {
        [Key]
        public int KotaId { get; set; }
        public int ProvinsiId { get; set; }
        public string NamaKota { get; set; }
    }
}