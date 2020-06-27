using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Rooster.Api
{
    public class RoosterService : Rooster.RoosterBase
    {
        private readonly ILogger<RoosterService> _logger;

        public RoosterService(ILogger<RoosterService> logger)
        {
            _logger = logger;
        }

        public override Task<ChangeNotificationResponse> Notify(ChangeNotificationRequest request,
            ServerCallContext context)
        {
            _logger.LogInformation(request.Value);
            return Task.FromResult(new ChangeNotificationResponse());
        }
    }
}