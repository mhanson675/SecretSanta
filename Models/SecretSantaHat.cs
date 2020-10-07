using System;
using System.Collections.Generic;
using System.Linq;

namespace SecretSanta.Models
{
    public class SecretSantaHat
    {
        private Random random = new Random();
        private Participant[] recipients;
        private Participant[] gifters;

        public SecretSantaHat(List<Participant> participants)
        {
            recipients = new Participant[participants.Count];
            gifters = new Participant[participants.Count];

            participants.CopyTo(recipients);
            participants.CopyTo(gifters);
        }

        public Dictionary<Participant, Participant> PullNames()
        {
            while (!IsValidPairs())
            {
                Shuffle(recipients);
                Shuffle(gifters);
            }

            var hatPulls = gifters.Zip(recipients, (key, value) => new { Key = key, Value = value }).ToDictionary(x => x.Key, x => x.Value);

            return hatPulls;
        }

        private bool IsValidPairs()
        {
            int len = recipients.Length;

            for (int i = 0; i < len; i++)
            {
                if (gifters[i].Id == recipients[i].Id)
                {
                    return false;
                }
            }

            return true;
        }

        private void Shuffle(Participant[] listToShuffle)
        {
            int len = listToShuffle.Length - 1;
            for (int i = len; i > 0; i--)
            {
                int randomIndex = random.Next(0, i + 1);

                var temp = listToShuffle[i];
                listToShuffle[i] = listToShuffle[randomIndex];
                listToShuffle[randomIndex] = temp;
            }
        }
    }
}
