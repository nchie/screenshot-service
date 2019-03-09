
using System;
using MassTransit;
using MassTransit.Util;
using Microsoft.Extensions.DependencyInjection;
using Screenshot.MQ.Messages;
using Screenshot.Service.MQ;
using Screenshot.Service.MQ.Consumer;

namespace Screenshot.Service.MQ
{
    static class Extensions 
    {
        public static IServiceCollection AddMq(this IServiceCollection services, MqConfiguration config){

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ScreenshotSavedConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(config.Host, "/", h => { });

                    cfg.ReceiveEndpoint(host, config.ScreenshotSavedEndpoint, e =>
                    {
                        e.PrefetchCount = 16;
                        //e.UseMessageRetry(x => x.Interval(2, 100));

                        e.Consumer<ScreenshotSavedConsumer>(provider);

                    });
                    EndpointConvention.Map<ScreenshotSaved>(host.Address.AppendToPath(config.ScreenshotSavedEndpoint));
                    EndpointConvention.Map<DownloadScreenshots>(host.Address.AppendToPath(config.ScreenshotRequestEndpoint));
                }));
            });
            
            services.AddHostedService<BusService>();

            return services;
        }

    }
}