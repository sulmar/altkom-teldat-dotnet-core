using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.IServices;

namespace Teldat.Vehicles.Infrastructure.FakeServices
{
    public class SmsMessageSender : IMessageSender
    {
        public Task SendAsync(string message)
        {
            Console.WriteLine($"Sending sms {message}");

            return Task.CompletedTask;
        }
    }
}
