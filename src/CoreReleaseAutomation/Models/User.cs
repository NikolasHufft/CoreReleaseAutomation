﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreReleaseAutomation.Models
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
