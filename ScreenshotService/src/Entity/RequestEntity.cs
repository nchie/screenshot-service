
using System;
using System.Collections.Generic;

namespace Screenshot.Service.Entity
{
    public class RequestEntity
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public DateTime DateTime { get; set; } = DateTime.Now;
        public IList<ScreenshotEntity> Screenshots { get; set; }

    }
}