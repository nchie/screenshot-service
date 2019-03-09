using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Screenshot.MQ.Messages;

namespace Screenshot.Service.Models
{
    public class RequestModel
    {
        public Guid Guid { get; set; }
        public DateTime DateTime { get; set; }
        public List<ScreenshotModel> Screenshots { get; set; }
    }
}
