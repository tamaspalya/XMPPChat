using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServiceWebApplication.Models
{
    /// <summary>
    /// A model class created in order to facilitate the presentation of multiple models on a razor page
    /// </summary>
    public class BigViewModel
    {
        public UserModel User { get; set; }
        public MessageModel Message { get; set; }
    }
}
