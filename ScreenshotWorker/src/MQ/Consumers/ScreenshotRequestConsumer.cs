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
        private IBrowser _browser;
        private ILogger _logger;
        public ScreenshotRequestConsumer(IBrowser browser, ILogger<ScreenshotRequestConsumer> logger)
        {
            _browser = browser;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<DownloadScreenshots> context)
        {
            _logger.LogDebug($"Consumed DownloadScreenshot with guid '{context.Message.Guid}'");
            foreach(var url in context.Message.Urls)
            {

                string cleanedUrl = Regex.Replace(url,"[^A-Za-z0-9. _]","");
                string dateString = Regex.Replace(DateTime.Now.ToString("u"), "[-: ]", "");
                string filename = dateString + "_" + cleanedUrl + ".png";
                _logger.LogDebug($"Screenshotting url '{url}' to file '{filename}'");
                await _browser.Screenshot(url, filename);
                _logger.LogDebug($"Success, sending saved message.");
                await context.Send<ScreenshotSaved>(new
                {
                    Guid = context.Message.Guid,
                    Success = true,
                    Url = url,
                    Filename = filename
                });
            }
        }
    }
}
