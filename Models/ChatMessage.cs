using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    [Keyless]
    public class ChatMessage
    {
        public string Message { get; set; }
        public DateTime date { get; set; }
        public string Email { get; set; }
        public string Group { get; set; }
    }
}