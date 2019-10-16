namespace VironIT_Social_network_server.BLL.DTO
{
    public class ContactedUserDTO : EntityDTO
    {
        public string ContactingUserId { get; set; }
        public string ContactedUserId { get; set; }
    }

    public class BlockedUserDTO : EntityDTO
    {
        public string BlockingUserId { get; set; }
        public string BlockedUserId { get; set; }
    }
}
