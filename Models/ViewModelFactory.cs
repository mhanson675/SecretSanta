using SecretSanta.ViewModels;

namespace SecretSanta.Models
{
    public static class ViewModelFactory
    {
        public static ParticipantViewModel Details(Participant participant)
        {
            return new ParticipantViewModel
            {
                Participant = participant,
                Action = "Details",
                ReadOnly = true,
                Theme = "info",
                ShowAction = false
            };
        }

        public static ParticipantViewModel Create(Participant participant)
        {
            return new ParticipantViewModel
            {
                Participant = participant
            };
        }

        public static ParticipantViewModel Edit(Participant participant)
        {
            return new ParticipantViewModel
            {
                Participant = participant,
                Action = "Edit",
                Theme = "warning"
            };
        }

        public static ParticipantViewModel Delete(Participant participant)
        {
            return new ParticipantViewModel
            {
                Participant = participant,
                Action = "Delete",
                ReadOnly = true,
                Theme = "danger"
            };
        }
    }
}
