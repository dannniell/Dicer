﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    public class CampaignModel
    {
        [Key]
        public int? CampaignId { get; set; }
        public string CampaignName { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
        public int Commission { get; set; }
        public string Task { get; set; }

        public string? CampaignImg { get; set; }

        public string? Gender { get; set; }

        public int? Kota { get; set; }

        public int? Provinsi { get; set; }
        public int? MinFollowers { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public string? UserId { get; set; }
        public string UserName { get; set; }
        public string Genre { get; set; }
    }
}
