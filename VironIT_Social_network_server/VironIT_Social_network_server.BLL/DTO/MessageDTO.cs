using System;


namespace VironIT_Social_network_server.BLL.DTO
{
    public class MessageDTO
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
    }
}