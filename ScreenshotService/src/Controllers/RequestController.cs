using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Screenshot.MQ.Messages;
using Screenshot.Service.Models;
using Screenshot.Service.Entity;

namespace Screenshot.Service.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private IBus _bus;
        private ScreenshotContext _context;

        public RequestController(IBus bus, ScreenshotContext context)
        {
            _bus = bus;
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestModel>>> Get()
        {
            return await _context.ScreenshotRequests
                .Include(e => e.Screenshots)
                .Select(e => e.ToModel())
                .ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{guid}")]
        public async Task<ActionResult<RequestModel>> Get(Guid guid)
        {
            var request = await _context.ScreenshotRequests
                .Include(e => e.Screenshots)
                .Select(e => e.ToModel())
                .FirstOrDefaultAsync(e => e.Guid == guid);
            if(request == null) return NotFound(); // TODO: Move exception handling somewhere better
            return request;
        }

        // POST api/values
        [HttpPost]
        public async Task<RequestEntity> Post([FromBody] PostRequest request)
        {
            // Create database entries for incoming request and save it
            var requestEntity = new RequestEntity {
                Screenshots = request.Urls.Select(u => new ScreenshotEntity { 
                    Url = u,
                    Status = "Processing"
                }).ToList()
            };
            _context.ScreenshotRequests.Add(requestEntity);
            await _context.SaveChangesAsync();

            // Send message to workers
            await _bus.Send<DownloadScreenshots>(new DownloadScreenshots
            {
                Guid = requestEntity.Guid,
                Urls = request.Urls
            });

            return requestEntity;
        }
    }
}
