namespace VironIT_Social_network_server.DAL.Model
{
    public struct AvatarSizeCategory
    {
        public static string Large = "Large";
        public static string Medium = "Medium";
    }

    public class Media : Entity
    {
        public string Link { get; set; }
        public string UserEmail { get; set; }
    }

    public class Image : Media { }
    public class Audio : Media { }
    public class Video : Media { }

    public class Avatar : Image
    {
        public string SizeCategory { get; set; }
    }
}
