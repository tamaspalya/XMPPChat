using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServiceWebApplication.Models
{
    public class ChatModel
    {
        public MessageModel Message { get; set; }
        public List<string> Archive { get; set; }
    }
}
