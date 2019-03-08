using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Screenshot.Messages;
using Screenshot.Service.Models;

namespace Screenshot.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenshotRequestController : ControllerBase
    {
        private IBus _bus;
        private Context _context;

        public ScreenshotRequestController(IBus bus, Context context)
        {
            _bus = bus;
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScreenshotRequestModel>>> Get()
        {
            return _context.ScreenshotRequests;
        }

        // GET api/values/5
        [HttpGet("{guid}")]
        public async Task<ActionResult<ScreenshotRequestModel>> Get(Guid guid)
        {
            var request = await _context.ScreenshotRequests.FirstOrDefaultAsync(e => e.Guid == guid);
            if(request == null) return NotFound();
            return request;
        }

        // POST api/values
        [HttpPost]
        public async Task<ScreenshotRequestModel> Post([FromBody] ScreenshotRequest request)
        {
            var requestEntity = new ScreenshotRequestModel();
            _context.ScreenshotRequests.Add(requestEntity);
            await _context.SaveChangesAsync();

            await _bus.Send<DownloadScreenshots>(new DownloadScreenshots
            {
                Guid = requestEntity.Guid,
                Urls = request.Urls
            });

            return requestEntity;
        }
    }
}
