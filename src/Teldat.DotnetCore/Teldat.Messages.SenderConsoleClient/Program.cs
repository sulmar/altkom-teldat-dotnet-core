using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

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
                .Build();

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            while (true)
            {
                await connection.SendAsync("SendAlarm", "Fire!");

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            Console.ResetColor();


        }
    }
}
