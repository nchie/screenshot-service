
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Screenshot.MQ.Messages;
using Screenshot.Service.Models;

namespace Screenshot.Service
{
    public interface IRequestHandler
    {
        Task UpdateRequest(ScreenshotSaved message);
        Task<RequestModel> GetRequest(Guid guid);
        Task<List<RequestModel>> GetRequests();
        Task<RequestModel> CreateRequest(IEnumerable<string> urls);
    }
}
