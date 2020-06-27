using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Rooster.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/rooster")
                .Build();

            connection.On<dynamic>("onChanged", (message) =>
            {
                Console.WriteLine(message.Message);
            });
            connection.Closed += async error =>
            {
                await Task.Delay(new Random().Next(0,5) * 1000);
                await connection.StartAsync();
            };
        }
    }
}