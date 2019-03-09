using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Util;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using PuppeteerSharp;

using Screenshot.Worker.MQ.Consumer;
using Screenshot.Worker.MQ;
using Screenshot.MQ.Messages;
using Screenshot.Worker.Browser;
using System.IO;

namespace Screenshot.Worker
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            await ConfigureHostBuilder(args).Build().RunAsync();
            // //var screenshotter = new Screenshotter(".");
            // //screenshotter.Screenshot("http://seboverflow.sebank.se/", "test.png").Wait();
        }
        static IHostBuilder ConfigureHostBuilder(string[] args)
        {
            return new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) => {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false);
                    config.AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    config.AddEnvironmentVariables(prefix: "SCREENSHOT:");
                })
                .ConfigureServices((hostingContext, services) => {
                    var mqConfig = hostingContext.Configuration.GetSection("Mq").Get<MqConfiguration>();
                    services.Configure<BrowserConfiguration>(hostingContext.Configuration.GetSection("Screenshotter"));

                    services.AddMq(mqConfig);
                    services.AddSingleton<IBrowser, PuppeteerBrowser>();
                    
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.SetMinimumLevel(LogLevel.Debug);
                });
        }
    }
}
