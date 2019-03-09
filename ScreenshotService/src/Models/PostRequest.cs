using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Screenshot.MQ.Messages;

namespace Screenshot.Service.Models
{
    public class PostRequest
    {
        public List<string> Urls { get; set; }
    }
}
