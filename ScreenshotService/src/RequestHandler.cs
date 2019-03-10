
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Screenshot.MQ.Messages;
using Screenshot.Service.Entity;
using Screenshot.Service.Models;

namespace Screenshot.Service
{
    class RequestHandler : IRequestHandler
    {
        private const string SCREENSHOT_PATH = "/screenshots/";
        private ScreenshotContext _dbContext;
        private IBus _bus;

        public RequestHandler(ScreenshotContext dbContext, IBus bus)
        {
            _dbContext = dbContext;
            _bus = bus;
        }


        public async Task UpdateRequest(ScreenshotSaved message)
        {
            var screenshot = _dbContext.Screenshots.Single(e => e.RequestGuid == message.Guid && e.Url == message.Url);
            screenshot.Path = Path.Combine(SCREENSHOT_PATH, message.Filename);
            screenshot.Status = "Success";

            await _dbContext.SaveChangesAsync();
        }
        public async Task<RequestModel> GetRequest(Guid guid)
        {
            var request = await _dbContext.ScreenshotRequests
                .Include(e => e.Screenshots)
                .Select(e => e.ToModel())
                .FirstOrDefaultAsync(e => e.Guid == guid);
            return request;
        }
        public async Task<List<RequestModel>> GetRequests()
        {
            return await _dbContext.ScreenshotRequests
                .Include(e => e.Screenshots)
                .Select(e => e.ToModel())
                .ToListAsync();
        }
        public async Task<RequestModel> CreateRequest(IEnumerable<string> urls)
        {
            // Create database entries for incoming request and save it
            var requestEntity = new RequestEntity {
                Screenshots = urls.Select(u => new ScreenshotEntity { 
                    Url = u,
                    Status = "Processing"
                }).ToList()
            };
            _dbContext.ScreenshotRequests.Add(requestEntity);
            await _dbContext.SaveChangesAsync();

            // Send message to workers
            await _bus.Send<DownloadScreenshots>(new DownloadScreenshots
            {
                Guid = requestEntity.Guid,
                Urls = urls.ToList()
            });

            return requestEntity.ToModel();
        }
    }
}
