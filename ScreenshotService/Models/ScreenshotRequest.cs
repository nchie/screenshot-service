using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Screenshot.Messages;

namespace Screenshot.Service.Models
{
    public class ScreenshotRequest
    {
        public List<string> Urls { get; set; }
    }
}
