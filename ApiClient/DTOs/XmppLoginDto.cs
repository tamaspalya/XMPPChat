﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient.DTOs
{
    public class XmppLoginDto
    {
        public string host { get; set; }
        public string user { get; set; }
        public string password { get; set; }
    }
}
