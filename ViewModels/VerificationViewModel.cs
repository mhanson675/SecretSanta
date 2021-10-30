using SecretSanta.Models;
using System.Collections.Generic;

namespace SecretSanta.ViewModels
{
    public class VerificationViewModel
    {
        public Dictionary<Participant, Participant> Paired { get; set; }
        public Dictionary<Participant, bool> Status { get; set; }

        public VerificationViewModel()
        {
            Paired = new Dictionary<Participant, Participant>();
            Status = new Dictionary<Participant, bool>();
        }

        public VerificationViewModel(Dictionary<Participant, Participant> pairs, Dictionary<Participant, bool> status)
        {
            Paired = pairs;
            Status = status;
        }

        public void AddPaired(Participant participant, Participant paired)
        {
            Paired.Add(participant, paired);
        }

        public void AddStatus(Participant participant, bool status)
        {
            Status.Add(participant, status);
        }
    }
}