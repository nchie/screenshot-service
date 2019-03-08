
using System;

namespace Screenshot.Service
{
    public class ScreenshotRequestModel
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public string File { get; set; }

    }
}