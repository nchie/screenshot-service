using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenshotService.Messages
{
    interface IScreenshotRequest
    {
        string RequestId { get; set; }
        string Url { get; set; }
    }
}
