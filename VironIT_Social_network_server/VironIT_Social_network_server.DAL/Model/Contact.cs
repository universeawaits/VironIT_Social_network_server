namespace VironIT_Social_network_server.DAL.Model
{
    public class ContactedUser : Entity
    {
        public string ContactingUserId { get; set; }
        public string ContactedUserId { get; set; }
    }

    public class BlockedUser : Entity
    {
        public string BlockingUserId { get; set; }
        public string BlockedUserId { get; set; }
    }

    public class Pseuonym : Entity
    {
        public string PseudoFromUserId { get; set; }
        public string PseudoForUserId { get; set; }
        public string Pseudonym { get; set; }
    }
}
