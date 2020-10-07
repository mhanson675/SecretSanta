using SecretSanta.Models;
using System.Collections.Generic;

namespace SecretSanta.ViewModels
{
    public class VerificationViewModel
    {
        public Dictionary<Participant, bool> Status { get; set; }

        public VerificationViewModel()
        {
            Status = new Dictionary<Participant, bool>();
        }
        public void AddStatus(Participant participant, bool status)
        {
            Status.Add(participant, status);
        }
    }
}
