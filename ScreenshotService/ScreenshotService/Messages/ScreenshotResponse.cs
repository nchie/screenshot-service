using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScreenshotService.Messages
{
    interface IScreenshotResponse
    {
        string RequestId { get; set; }
        bool Success { get; set; }
        string File { get; set; }
    }
}
