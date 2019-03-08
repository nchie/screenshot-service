using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Service
{
    public class BackgroundWorkerService : BackgroundService
    {
        private readonly ILogger _logger;
        // private readonly AppSettings _settings;
        private IBusControl _busControl;

        public BackgroundWorkerService(ILoggerFactory loggerFactory, IBusControl busControl/*, IOptionsSnapshot<AppSettings> options */)
        {
            this._logger = loggerFactory.CreateLogger<BackgroundWorkerService>();
            // this._settings = options.Value;
            _busControl = busControl;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping message queue");
            await _busControl.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //await _busControl.StartAsync(stoppingToken);
            _busControl.Start();

            return Task.CompletedTask;

            //while (!stoppingToken.IsCancellationRequested)
            //{
            //    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            //    Console.WriteLine("hej");
            //    //_logger.LogInformation($"Printer2 is working. {_settings.PrinterDelaySecond}");
            //    //await Task.Delay(TimeSpan.FromSeconds(_settings.PrinterDelaySecond), stoppingToken);
            //}
        }
    }
}
