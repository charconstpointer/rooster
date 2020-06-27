using System.Collections.Generic;
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
        private readonly ICollection<string> _timestamps;

        public RoosterController(IDistributedCache distributedCache, ICollection<string> timestamps)
        {
            _distributedCache = distributedCache;
            _timestamps = timestamps;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var value = await _distributedCache.GetStringAsync("sweets");
            return Ok(value);
        }

        [HttpGet("timestamps")]
        public async Task<IActionResult> GetTimestamps()
        {
            return Ok(_timestamps);
        }
    }
}