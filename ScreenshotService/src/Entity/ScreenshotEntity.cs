
using System;

namespace Screenshot.Service.Entity
{
    public class ScreenshotEntity
    {
        public Guid RequestGuid { get; set; }
        public string Url { get; set; }
        public string Status { get; set; }
        public string Path { get; set; }
    }
}