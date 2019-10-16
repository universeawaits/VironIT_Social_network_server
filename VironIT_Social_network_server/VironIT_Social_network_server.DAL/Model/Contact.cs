namespace VironIT_Social_network_server.DAL.Model
{
    public class ContactUser : Entity
    {
        public string ContactingUserId { get; set; }
        public string ContactedUserId { get; set; }
    }

    public class BlockedUser : Entity
    {
        public string BlockingUserId { get; set; }
        public string BlockedUserId { get; set; }
    }
}
