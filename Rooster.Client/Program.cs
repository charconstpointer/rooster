using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Rooster.Api.Messages;

namespace Rooster.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/rooster-ws")
                .Build();

            connection.On<Notification>("onChanged", message =>
            {
                Console.WriteLine(message.Body);
            });
            connection.Closed += async error =>
            {
                await Task.Delay(new Random().Next(0,5) * 1000);
                await connection.StartAsync();
            };
            await connection.StartAsync();
            await Task.Delay(9999999);
        }
    }
}