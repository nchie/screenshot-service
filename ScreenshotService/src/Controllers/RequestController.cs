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
        private IRequestHandler _requestHandler;

        public RequestController(IBus bus, ScreenshotContext context, IRequestHandler requests)
        {
            _bus = bus;
            _context = context;
            _requestHandler = requests;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IList<RequestModel>>> Get()
        {
            var requests = await _requestHandler.GetRequests();
            return requests;
        }

        // GET api/values/5
        [HttpGet("{guid}")]
        public async Task<ActionResult<RequestModel>> Get(Guid guid)
        {
            var request = await _requestHandler.GetRequest(guid);
            if(request == null) return NotFound(); // TODO: Move exception handling to a middleware?
            return request;
        }

        // POST api/values
        [HttpPost]
        public async Task<RequestModel> Post([FromBody] PostRequest request)
        {
            return await _requestHandler.CreateRequest(request.Urls);
        }
    }
}
