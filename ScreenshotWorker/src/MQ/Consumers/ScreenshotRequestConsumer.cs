using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Screenshot.MQ.Messages;
using Screenshot.Worker.Browser;

namespace Screenshot.Worker.MQ.Consumer
{
    class ScreenshotRequestConsumer : IConsumer<DownloadScreenshots>
    {
        private ILogger _logger;
        private IRequestHandler _handler;
        public ScreenshotRequestConsumer(IRequestHandler handler, ILogger<ScreenshotRequestConsumer> logger)
        {
            _logger = logger;
            _handler = handler;
        }
        public async Task Consume(ConsumeContext<DownloadScreenshots> context)
        {
            _logger.LogDebug($"Consumed DownloadScreenshot with guid '{context.Message.Guid}'");
            await _handler.HandleRequest(context.Message, context);
        }
    }
}
