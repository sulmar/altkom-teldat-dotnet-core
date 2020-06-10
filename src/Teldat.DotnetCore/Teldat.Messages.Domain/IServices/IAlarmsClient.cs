using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teldat.Messages.Domain.Models;

namespace Teldat.Messages.Domain.IServices
{
    public interface IAlarmsClient
    {
        Task Pong();
        Task Alarm(string message);
        Task Command(Command command);
    }
}
