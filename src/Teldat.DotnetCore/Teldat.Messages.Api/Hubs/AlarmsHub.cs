using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teldat.Messages.Domain.IServices;
using Teldat.Messages.Domain.Models;

namespace Teldat.Messages.Api.Hubs
{
    public class StrongTypedAlarmsHub : Hub<IAlarmsClient>
    {
        public override async Task OnConnectedAsync()
        {
            //string unit = "A";

            //await Groups.AddToGroupAsync(Context.ConnectionId, unit);

            await base.OnConnectedAsync();
        }

        public async Task JoinToUnit(string unit)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, unit);
        }

        public async Task SendAlarm(string message)
        {
            await Clients.All.Alarm(message);
        }

        public async Task SendCommand(Command command)
        {
            // await Clients.Others.Command(command);

            await Clients.Groups(command.ToUnit).Command(command);
        }

        public async Task Ping()
        {
            await Clients.Caller.Pong();
        }
    }

    public class AlarmsHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task SendAlarm(string message)
        {
            await Clients.All.SendAsync("Alarm", message);
        }

        public async Task SendCommand(Command command)
        {
            await Clients.Others.SendAsync("Command", command);
        }

        public async Task Ping()
        {
            await Clients.Caller.SendAsync("Pong");
        }
    }
}
