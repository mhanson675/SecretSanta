using SecretSanta.Models;

namespace SecretSanta.ViewModels
{
    public class ParticipantViewModel
    {
        public Participant Participant { get; set; }
        public string Action { get; set; } = "Create";
        public bool ReadOnly { get; set; } = false;
        public string Theme { get; set; } = "primary";
        public bool ShowAction { get; set; } = true;
    }
}
