using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Teldat.Vehicles.Api.Events;

namespace Teldat.Vehicles.Api.Handlers
{
    public class LoggerVehicleHandler : INotificationHandler<AddVehicleEvent>
    {
        private readonly ILogger<LoggerVehicleHandler> logger;

        public LoggerVehicleHandler(ILogger<LoggerVehicleHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(AddVehicleEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Vehicle {notification.Vehicle.Model} was added");

            return Task.CompletedTask;
        }
    }
}
