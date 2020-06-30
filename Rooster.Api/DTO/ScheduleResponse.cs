using System.Collections.Generic;

namespace Rooster.Api.DTO
{
    public class ScheduleResponse
    {
        public IEnumerable<Program> Schedule { get; set; }
    }
}