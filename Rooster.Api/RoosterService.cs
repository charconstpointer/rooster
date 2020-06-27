using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Rooster.Api
{
    public class RoosterService : Rooster.RoosterBase
    {
        private readonly ILogger<RoosterService> _logger;
        private readonly ICollection<string> _timestamps;

        public RoosterService(ILogger<RoosterService> logger, ICollection<string> timestamps)
        {
            _logger = logger;
            _timestamps = timestamps;
        }

        public override Task<ChangeNotificationResponse> Notify(ChangeNotificationRequest request,
            ServerCallContext context)
        {
            _logger.LogInformation(request.Value);
            _timestamps.Add(request.Value);
            return Task.FromResult(new ChangeNotificationResponse());
        }
    }
}