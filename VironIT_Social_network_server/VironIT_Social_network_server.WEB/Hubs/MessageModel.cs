using System;


namespace VironIT_Social_network_server.WEB.SignalR
{
    public class MessageModel
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string ForwardFromEmail { get; set; }
        public string Type { get; set; }
        public MessageMediaModel MessageMedia { get; set; }
    }

    public class MessageMediaModel
    {
        public int MediaId { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
    }
}
