using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teldat.Messages.Api.Hubs
{
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

        public async Task SendCommand(string content)
        {
            await Clients.Others.SendAsync("Command", content);
        }

        public async Task Ping()
        {
            await Clients.Caller.SendAsync("Pong");
        }
    }
}
