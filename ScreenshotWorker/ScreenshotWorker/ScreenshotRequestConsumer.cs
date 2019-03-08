using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Service.Messages;

namespace Screenshotter.Worker
{
    class ScreenshotRequestConsumer : IConsumer<ScreenshotRequest>
    {
        public Task Consume(ConsumeContext<ScreenshotRequest> context)
        {
            Console.WriteLine("Consumed!");
            return Task.CompletedTask;

            //await context.Publish<IScreenshotResponse>(new
            //{
            //    RequestId = context.Message.RequestId,
            //    Success = true,
            //    File = "test"
            //});
        }
    }
}
