﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Persistence.Auth
{
    public class JwtTokensDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
