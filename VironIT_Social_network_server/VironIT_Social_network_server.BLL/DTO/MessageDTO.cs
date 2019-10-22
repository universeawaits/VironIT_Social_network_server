using System;


namespace VironIT_Social_network_server.BLL.DTO
{
    public class MessageDTO : EntityDTO
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
        public string ForwardFromEmail { get; set; }
        public string Type { get; set; }

        public MessageMediaDTO MessageMedia { get; set; }
    }

    public class MessageMediaDTO : EntityDTO
    {
        public int MediaId { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
    }
}