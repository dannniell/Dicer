using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    public class ChatMessage
    {
        [Key]
        public int? ChatId { get; set; }
        public string Message { get; set; }
        public DateTime date { get; set; }
        public string Email { get; set; }
        public string Group { get; set; }
    }
}