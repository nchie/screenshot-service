using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Screenshot.Messages
{
    class DownloadScreenshots
    {
        public Guid Guid { get; set; }
        public List<string> Urls { get; set; }
    }
}
