using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Screenshot.MQ.Messages
{
    public class ScreenshotSaved
    {
        public Guid Guid { get; set; }
        public string Url { get; set; }
        public string Filename { get; set; }
    }
}
