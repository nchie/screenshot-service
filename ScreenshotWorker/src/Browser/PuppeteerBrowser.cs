using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Screenshot.Worker.Browser
{
    // Simply wraps Puppeteer.Browser
    class PuppeteerBrowser : IBrowser
    {
        private readonly string _outputDir;
        private PuppeteerSharp.Browser _browser;
        private IOptionsMonitor<BrowserConfiguration> _options;
        private ILogger _logger;
        public PuppeteerBrowser(IOptionsMonitor<BrowserConfiguration> optionsAccessor, ILogger<PuppeteerBrowser> logger) 
        {
            _logger = logger;
            _options = optionsAccessor;
            _outputDir = _options.CurrentValue.OutputDirectory;
            // This can definitely fail and probably shouldn't be in a constructor, but...
            _browser = Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new [] {"--no-sandbox", "--disable-setuid-sandbox" }
            }).Result;
        }
        public async Task Screenshot(string uri, string outputFile)
        {
            outputFile = Path.Combine(_outputDir, outputFile);
            var options = _options.CurrentValue;
            var page = await _browser.NewPageAsync();

            await page.GoToAsync(uri);
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = options.PageWidth,
                Height = options.PageHeight
            });
            _logger.LogDebug($"Screenshotting url '{uri}' to file '{outputFile}'");
            // TODO: Async wait x seconds since the page might not display correctly right away?
            await page.ScreenshotAsync(outputFile);
            _logger.LogDebug($"Success, sending saved message.");
            await page.CloseAsync();
        }
    }
}
