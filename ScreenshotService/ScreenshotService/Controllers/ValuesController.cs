using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ScreenshotService.Messages;
using ScreenshotService.Models;

namespace ScreenshotService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IBus _bus;

        public ValuesController(IBus bus)
        {
            _bus = bus;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] ScreenshotRequest request)
        {
            var endpoint = await _bus.GetSendEndpoint(new Uri("rabbitmq://localhost/screenshot_request_send"));

            await endpoint.Send<IScreenshotRequest>(new ScreenshotRequestMessage
            {
                RequestId = "requestId",
                Url = "url"
            });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
