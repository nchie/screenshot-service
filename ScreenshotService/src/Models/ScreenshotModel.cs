using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Screenshot.MQ.Messages;

namespace Screenshot.Service.Models
{
    public class ScreenshotModel 
    {
        public string Url { get; set; }
        public string Status { get; set; }
        public string Path { get; set; }
    }

}
