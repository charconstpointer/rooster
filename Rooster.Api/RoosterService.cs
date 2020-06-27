using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Rooster.Api.Hubs;
using Rooster.Api.Messages;

namespace Rooster.Api
{
    public class RoosterService : Rooster.RoosterBase
    {
        private readonly ILogger<RoosterService> _logger;
        private readonly ICollection<string> _timestamps;
        private readonly IHubContext<RoosterHub> _hub;

        public RoosterService(ILogger<RoosterService> logger, ICollection<string> timestamps,
            IHubContext<RoosterHub> hub)
        {
            _logger = logger;
            _timestamps = timestamps;
            _hub = hub;
        }

        public override Task<ChangeNotificationResponse> Notify(ChangeNotificationRequest request,
            ServerCallContext context)
        {
            _logger.LogInformation(request.Value);
            _timestamps.Add(request.Value);
            _hub.Clients.All.SendAsync("onChanged", new Notification {Body = request.Value});
            _logger.LogInformation($"Pushed onChanged ({request.Value}) via SignalR");
            return Task.FromResult(new ChangeNotificationResponse());
        }
    }
}