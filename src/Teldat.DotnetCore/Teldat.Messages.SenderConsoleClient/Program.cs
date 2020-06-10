using Bogus;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Teldat.Messages.Domain.Models;

namespace Teldat.Messages.SenderConsoleClient
{
    class Program
    {

        // dotnet add package Microsoft.AspNetCore.SignalR.Client

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello Signal-R Sender!");

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;

            const string url = "http://localhost:5000/signalr/alarms";

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .Build();

            await connection.StartAsync();

            // dotnet add package Bogus

            IEnumerable<Command> commands = new Faker<Command>()
                .RuleFor(p => p.Title, f => f.Lorem.Sentence())
                .RuleFor(p => p.Content, f => f.Lorem.Paragraph())
                .GenerateForever();

            Console.WriteLine("Connected.");

            Console.Write("Type unit:");
            string unit = Console.ReadLine();

            await connection.SendAsync("JoinToUnit", unit);

            foreach(Command command in commands)
            {
                // await connection.SendAsync("SendAlarm", "Fire!");
                command.ToUnit = unit;
                await connection.SendAsync("SendCommand", command);
                Console.WriteLine($"Sent command {command.Title}");

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();


        }
    }
}
