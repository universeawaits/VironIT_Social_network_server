using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VironIT_Social_network_server.WEB.ViewModel
{
    public class ContactModel
    {
        public string ContactingUserId { get; set; }
        public string ContactedUserId { get; set; }
    }

    public class BlockModel
    {
        public string BlockingUserId { get; set; }
        public string BlockedUserId { get; set; }
    }

    public class PseudonymModel
    {
        public string PseudoFromUserId { get; set; }
        public string PseudoForUserId { get; set; }
        public string PseudonymRaw { get; set; }
    }
}
