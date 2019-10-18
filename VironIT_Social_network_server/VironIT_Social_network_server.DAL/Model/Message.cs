using System;


namespace VironIT_Social_network_server.DAL.Model
{
    public class Message : Entity
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public DateTime DateTime { get; set; }
        public string Text { get; set; }
    }
}
