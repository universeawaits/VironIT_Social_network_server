namespace VironIT_Social_network_server.BLL.DTO
{
    public class ImageDTO : EntityDTO
    {
        public string Link { get; set; }
        public string UserEmail { get; set; }
    }

    public class AvatarDTO : ImageDTO
    {
        public string SizeCategory { get; set; }
    }
}
