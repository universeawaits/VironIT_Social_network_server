using System;


namespace VironIT_Social_network_server.DAL.Model
{
    public struct MessageType
    {
        public static string Text = "Text";
        public static string Contact = "Contact";
    }

    public struct MediaType
    {
        public static string Audio = "Audio";
        public static string Video = "Video";
        public static string Image = "Image";
    }

    public class Message : Entity
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public string ForwardFromEmail { get; set; }
        public string Type { get; set; }

        public MessageMedia MessageMedia { get; set; }
        public int? MessageMediaId { get; set; }
    }

    public class MessageMedia : Entity
    {
        public int MediaId { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }

        public Message Message { get; set; }
    }
}
