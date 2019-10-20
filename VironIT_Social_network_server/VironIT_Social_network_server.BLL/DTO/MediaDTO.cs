namespace VironIT_Social_network_server.BLL.DTO
{
    public class MediaDTO : EntityDTO
    {
        public string Link { get; set; }
        public string UserEmail { get; set; }
    }

    public class ImageDTO : MediaDTO { }
    public class AudioDTO : MediaDTO { }
    public class VideoDTO : MediaDTO { }

    public class AvatarDTO : ImageDTO
    {
        public string SizeCategory { get; set; }
    }
}
