﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class Config
    {
        public string RabbitMqHost { get; set; }
        public string Queue { get; set; }
        public string OutputDirectory { get; set; }
    }
}
