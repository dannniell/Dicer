using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    [Keyless]
    public class ClientWithdrawl
    {
        public int Total { get; set; }
    }
}