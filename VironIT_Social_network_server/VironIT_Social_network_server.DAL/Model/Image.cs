namespace VironIT_Social_network_server.DAL.Model
{
    public struct SizeCategory
    {
        public static string Large = "Large";
        public static string Medium = "Medium";
    }

    public class Image : Entity
    {
        public string Link { get; set; }
        public string UserEmail { get; set; }
    }

    public class Avatar : Image {
        public string SizeCategory { get; set; }
    }
}
