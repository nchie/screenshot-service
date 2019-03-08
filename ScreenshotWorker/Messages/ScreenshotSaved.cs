using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Screenshot.Messages
{
    class ScreenshotSaved
    {
        public Guid Guid { get; set; }
        public bool Success { get; set; }
        public string File { get; set; }
    }
}
