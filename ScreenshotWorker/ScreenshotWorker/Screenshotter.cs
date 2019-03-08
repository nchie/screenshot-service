using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Screenshotter.Worker
{
    class Screenshotter
    {
        private readonly string _outputDir;

        public Screenshotter(string outputDir)
        {
            _outputDir = outputDir;
        }
        public async Task Screenshot(string uri, string outputFile)
        {
            //await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                //ExecutablePath = "C:\\chrome-win\\chrome.exe"
                Args = new [] {"--no-sandbox", "--disable-setuid-sandbox" }
            });
            // TODO: Don't open and close browser on every screenshot as it's wasteful?
            var page = await browser.NewPageAsync();
            await page.GoToAsync(uri);
            // TODO: Async wait x seconds since the page might not display correctly right away?
            await page.ScreenshotAsync(Path.Combine(_outputDir, outputFile));
            await browser.CloseAsync();
        }
    }
}
