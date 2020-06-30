using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rooster.Api.DTO;
using Rooster.Api.Extensions;

namespace Rooster.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchedulesController : ControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id, DateTime day)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(
                $"https://polskie.azurewebsites.net/mobile/api/schedules/?Program={id}&SelectedDate={day}");
            var schedule = JsonConvert.DeserializeObject<ScheduleResponse>(response);
            var programs = schedule.Schedule.AsDto();
            return Ok(programs);
        }
    }
}