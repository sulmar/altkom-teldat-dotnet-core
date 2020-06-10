using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using Teldat.Messages.Domain.Models;

namespace Teldat.Messages.ReceiverConsoleClient
{
    class Program
    {
        // dotnet add package Microsoft.AspNetCore.SignalR.Client

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Signal-R Receiver!");

            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            const string url = "http://localhost:5000/signalr/alarms";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            Console.Write("Type unit:");
            string unit = Console.ReadLine();

            await connection.SendAsync("JoinToUnit", unit);

            connection.On<string>("Alarm", message => Console.WriteLine($"Received {message}"));

            connection.On<Command>("Command", command => Console.WriteLine($"Received {command.Title} -> {command.Content}"));

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();
        }
    }
}
