using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    public class CreatorJob
    {
        [Key]
        public string UserId { get; set; }
        public int CampaignId { get; set; }

        public bool IsOnProgress { get; set; }

        public bool IsAccepted { get; set; }

        public bool IsDeclined { get; set; }

        public bool IsDone { get; set; }
    }
}
