namespace VironIT_Social_network_server.WEB.ViewModel
{
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
