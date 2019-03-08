using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Messages
{
    class ScreenshotRequest : IScreenshotRequest
    {
        public string RequestId { get; set; }
        public string Url { get; set; }
    }
}
