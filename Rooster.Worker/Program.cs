using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Rooster.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = "localhost";
                        options.InstanceName = "Rooster";
                    });
                    services.AddHostedService<Worker>();
                });
    }
}