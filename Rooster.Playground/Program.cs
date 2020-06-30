using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Rooster.Playground
{
    public class ScheduleResponse
    {
        public IEnumerable<Program> Schedule { get; set; }
    }

    public class Program
    {
        public int AntenaId { get; set; }
        public string ArticleLink { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public dynamic Leaders { get; set; }
        public string Photo { get; set; }
        public dynamic Sounds { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime StopHour { get; set; }
        public IEnumerable<SubProgram> Subprograms { get; set; }
    }

    public class SubProgram
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Photo { get; set; }
        public string Title { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime StopHour { get; set; }
        public dynamic Leaders { get; set; }
        public dynamic Sounds { get; set; }
    }

    public class ProgramDto
    {
        public int AntenaId { get; set; }
        public string ArticleLink { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
        public dynamic Leaders { get; set; }
        public string Photo { get; set; }
        public dynamic Sounds { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime StopHour { get; set; }
    }

    class App
    {
        static async Task Main()
        {
            var httpClient = new HttpClient();
            // var stream = await httpClient.GetStreamAsync(
            // "https://polskie.azurewebsites.net/mobile/api/schedules/?Program=3&SelectedDate=2020.05.05");
            // var schedule = await JsonSerializer.DeserializeAsync<ScheduleResponse>(stream);
            var response = await httpClient.GetStringAsync(
                "https://polskie.azurewebsites.net/mobile/api/schedules/?Program=3&SelectedDate=2020.05.05");
            var schedule = JsonConvert.DeserializeObject<ScheduleResponse>(response);
            var programs = schedule.Schedule.AsDto();
            foreach (var programDto in programs)
            {
                Console.WriteLine(
                    $"{programDto.Id} {programDto.Title} {programDto.Description} {programDto.StartHour} {programDto.StopHour}");
            }
        }
    }
}