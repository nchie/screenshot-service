using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScreenshotService.Messages;

namespace ScreenshotService.Models
{
    public class ScreenshotRequest
    {
        public List<string> Urls { get; set; }
    }
}
