namespace VironIT_Social_network_server.BLL.DTO
{
    public class ContactDTO : EntityDTO
    {
        public string ContactingUserId { get; set; }
        public string ContactedUserId { get; set; }
    }

    public class BlockDTO : EntityDTO
    {
        public string BlockingUserId { get; set; }
        public string BlockedUserId { get; set; }
    }

    public class PseudonymDTO : EntityDTO
    {
        public string PseudoFromUserId { get; set; }
        public string PseudoForUserId { get; set; }
        public string PseudonymRaw { get; set; }
    }
}
