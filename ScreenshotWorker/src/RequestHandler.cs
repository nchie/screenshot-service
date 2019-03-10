using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Screenshot.MQ.Messages;
using Screenshot.Worker.Browser;

namespace Screenshot.Worker
{
    class RequestHandler : IRequestHandler
    {
        private IBrowser _browser;
        private ILogger _logger;
        public RequestHandler(IBrowser browser, ILogger<RequestHandler> logger)
        {
            _browser = browser;
            _logger = logger;
        }

        public async Task HandleRequest(DownloadScreenshots message, ConsumeContext context)
        {
            foreach(var uri in message.Urls)
            {
                // Clean url and date string
                string cleanedUrl = Regex.Replace(uri,"[^A-Za-z0-9. _]","");
                string dateString = Regex.Replace(DateTime.Now.ToString("u"), "[-: ]", "");
                string filename = dateString + "_" + cleanedUrl + ".png";
                await _browser.Screenshot(uri, filename);
                await context.Send<ScreenshotSaved>(new
                {
                    Guid = message.Guid,
                    Success = true,
                    Url = uri,
                    Filename = filename
                });
            }
        }
    }
}
