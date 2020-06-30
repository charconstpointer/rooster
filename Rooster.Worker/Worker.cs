using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Net.Client;
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
            var channel = GrpcChannel.ForAddress("http://api:5000");
            var client = new Rooster.RoosterClient(channel);
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var value = DateTime.UtcNow.Millisecond.ToString();
                await _cache.SetStringAsync("sweets", value, stoppingToken);
                await client.NotifyAsync(new ChangeNotificationRequest {Value = value});
                _logger.LogInformation("Notification sent");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}