using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using Screenshot.Messages;

namespace Screenshot.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ScreenshotSavedConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host("localhost", "/", h => { });


                    cfg.ReceiveEndpoint(host, "screenshot-saved", e =>
                    {
                        e.PrefetchCount = 16;
                        //e.UseMessageRetry(x => x.Interval(2, 100));

                        e.Consumer<ScreenshotSavedConsumer>(provider);

                    });

                    EndpointConvention.Map<ScreenshotSaved>(host.Address.AppendToPath("screenshot-saved"));
                    EndpointConvention.Map<DownloadScreenshots>(host.Address.AppendToPath("submit-screenshot-request"));
                }));

                // x.AddRequestClient<SubmitOrder>();
            });
            services.AddHostedService<BusService>();

            services.AddDbContext<Context>(opt => opt.UseInMemoryDatabase("RequestDb"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
