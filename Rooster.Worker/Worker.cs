using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Rooster.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IDistributedCache _cache;

        public Worker(ILogger<Worker> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await _cache.SetStringAsync("sweets", DateTime.UtcNow.Millisecond.ToString(), token: stoppingToken);
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}