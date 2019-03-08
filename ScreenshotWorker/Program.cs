using PuppeteerSharp;
using System;
using MassTransit;
using Screenshot.Messages;
using MassTransit.Util;

namespace Screenshot.Worker
{
    class Program
    {
        static void Main(string[] args)
        {

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "submit-screenshot-request", e =>
                {
                    e.PrefetchCount = 16;
                    //e.UseMessageRetry(x => x.Interval(2, 100));
                    e.Consumer<ScreenshotRequestConsumer>();
                });

                EndpointConvention.Map<ScreenshotSaved>(host.Address.AppendToPath("screenshot-saved"));
                EndpointConvention.Map<DownloadScreenshots>(host.Address.AppendToPath("submit-screenshot-request"));
            });

            bus.Start();


            //var screenshotter = new Screenshotter(".");
            //screenshotter.Screenshot("http://seboverflow.sebank.se/", "test.png").Wait();
        }
    }
}
