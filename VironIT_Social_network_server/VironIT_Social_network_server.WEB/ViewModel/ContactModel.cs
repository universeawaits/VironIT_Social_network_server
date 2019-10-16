using System;

namespace VironIT_Social_network_server.WEB.ViewModel
{
    public class ContactProfileModel
    {
        public UserProfileModel User { get; set; }

        public string Pseudonym { get; set; }
        public bool IsOnline { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsContact { get; set; }
        public DateTime LastSeen { get; set; }
    }

    public class ContactModel
    {
        public string ContactingUserEmail { get; set; }
        public string ContactedUserEmail { get; set; }
    }

    public class BlockModel
    {
        public string BlockingUserEmail { get; set; }
        public string BlockedUserEmail { get; set; }
    }

    public class PseudonymModel
    {
        public string PseudoFromUserEmail { get; set; }
        public string PseudoForUserEmail { get; set; }
        public string PseudonymRaw { get; set; }
    }
}
