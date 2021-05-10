using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using HackerNewsModule.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNewsModule.Utils
{
    public class TimedHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private Timer _timer;
        public IServiceProvider Services { get; }


        public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void DoWork(object state)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedHackerNewsService =
                    scope.ServiceProvider
                        .GetRequiredService<IHackerNewsService>();
                scopedHackerNewsService.DoWork();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
    }
}
