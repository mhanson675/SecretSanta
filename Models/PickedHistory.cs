using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecretSanta.Models
{
    public class PickedHistory
    {
        public int Id { get; set; }
        public DateTime PulledOn { get; set; }

        public int GifterId { get; set; }
        public Participant Gifter { get; set; }

        public int RecipientId { get; set; }
        public Participant Recipient { get; set; }
    }
}