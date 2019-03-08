using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenshotService.Messages
{
    class ScreenshotRequestMessage : IScreenshotRequest
    {
        public string RequestId { get; set; }
        public string Url { get; set; }
    }
}
