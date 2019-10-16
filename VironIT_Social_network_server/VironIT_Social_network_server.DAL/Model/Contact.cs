namespace VironIT_Social_network_server.DAL.Model
{
    public class Contact : Entity
    {
        public string ContactingUserId { get; set; }
        public string ContactedUserId { get; set; }
    }

    public class Block : Entity
    {
        public string BlockingUserId { get; set; }
        public string BlockedUserId { get; set; }
    }

    public class Pseudonym : Entity
    {
        public string PseudoFromUserId { get; set; }
        public string PseudoForUserId { get; set; }
        public string PseudonymRaw { get; set; }
    }
}
