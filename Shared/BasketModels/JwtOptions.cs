﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BasketModels
{
    public class JwtOptions
    {
        public string Secretkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DuartionInDays { get; set; }
    }
}