using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Rooster.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoosterController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;

        public RoosterController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var value = await _distributedCache.GetStringAsync("sweets");
            return Ok(value);
        }
    }
}