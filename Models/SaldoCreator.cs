using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    [Keyless]
    public class SaldoCreator
    {
        public string UserId { get; set; }
        public long Saldo { get; set; }
    }
}