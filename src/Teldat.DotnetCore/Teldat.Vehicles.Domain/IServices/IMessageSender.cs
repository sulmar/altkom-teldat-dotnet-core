using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Teldat.Vehicles.Domain.IServices
{
    public interface IMessageSender
    {
        Task SendAsync(string message);
    }
}
