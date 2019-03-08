using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Service.Messages;

namespace Service
{
    class ScreenshotRequestConsumer : IConsumer<IScreenshotRequest>
    {
        public async Task Consume(ConsumeContext<IScreenshotRequest> context)
        {
            Console.WriteLine("Consumed!");

            await context.Publish<IScreenshotResponse>(new
            {
                RequestId = context.Message.RequestId,
                Success = true,
                File = "test"
            });
        }
    }
}
