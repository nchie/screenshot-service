using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Screenshot.Messages;

namespace Screenshot.Worker
{
    class ScreenshotRequestConsumer : IConsumer<DownloadScreenshots>
    {
        public async Task Consume(ConsumeContext<DownloadScreenshots> context)
        {
            Console.WriteLine("Consumed!");
            //Console.WriteLine(context.Message.Urls[0]);
            //return Task.CompletedTask;

            await context.Send<ScreenshotSaved>(new
            {
               Guid = context.Message.Guid,
               Success = true,
               File = "test"
            });
        }
    }
}
