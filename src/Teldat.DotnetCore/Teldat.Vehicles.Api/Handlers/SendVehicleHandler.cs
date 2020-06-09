using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Teldat.Vehicles.Api.Events;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Api.Handlers
{
    public class SendVehicleHandler : INotificationHandler<AddVehicleEvent>
    {
        private readonly IMessageSender messageSender;

        public SendVehicleHandler(IMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        public async Task Handle(AddVehicleEvent notification, CancellationToken cancellationToken)
        {
            Vehicle vehicle = notification.Vehicle;
            await messageSender.SendAsync($"Vehicle {vehicle.Model} was added");
        }
    }


}
