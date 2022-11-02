using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    public class ClientCampaign
    {
        [Key]
        public string UserId { get; set; }
        public int CampaignId { get; set; }
    }
}
