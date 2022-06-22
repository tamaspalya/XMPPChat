using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServiceWebApplication.Models
{
    public class MessageModel
    {
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Content { get; set; }
    }
}
