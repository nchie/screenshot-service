using PuppeteerSharp;
using System;
using MassTransit;

namespace Screenshotter.Worker
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
                //var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                //{
                //    h.Username("guest");
                //    h.Password("guest");
                //});

                cfg.ReceiveEndpoint(host, "screenshot_request2", e =>
                {
                    e.Consumer<ScreenshotRequestConsumer>();
                    e.Consumer(() => new ScreenshotRequestConsumer());
                });
            });

            bus.Start();


            //var screenshotter = new Screenshotter(".");
            //screenshotter.Screenshot("http://seboverflow.sebank.se/", "test.png").Wait();
        }
    }
}
