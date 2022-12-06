using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dicer.Models
{
    public class AcceptParticipant
    {
        public List<AcceptedUser> users { get; set; }

        public long Paid { get; set; }
    }

    public class AcceptedUser
    {
        public string userId { get; set; }
    }
}