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
        public PuppeteerBrowser(IOptionsMonitor<BrowserConfiguration> optionsAccessor) 
        {
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
            var options = _options.CurrentValue;
            var page = await _browser.NewPageAsync();

            await page.GoToAsync(uri);
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = options.PageWidth,
                Height = options.PageHeight
            });
            // TODO: Async wait x seconds since the page might not display correctly right away?
            await page.ScreenshotAsync(Path.Combine(_outputDir, outputFile));
            await page.CloseAsync();
        }
    }
}
