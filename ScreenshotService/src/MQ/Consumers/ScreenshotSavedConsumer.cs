using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Screenshot.MQ.Messages;
using Screenshot.Service.Entity;

namespace Screenshot.Service.MQ.Consumer
{
    internal class ScreenshotSavedConsumer : IConsumer<ScreenshotSaved>
    {
        private ScreenshotContext _dbContext;
        private IRequestHandler _requestHandler;
        public ScreenshotSavedConsumer(IRequestHandler requestHandler)
        {
            _requestHandler = requestHandler;
        }
        public async Task Consume(ConsumeContext<ScreenshotSaved> context)
        {
            await _requestHandler.UpdateRequest(context.Message);
        }
    }
}