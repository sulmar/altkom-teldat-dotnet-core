using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Teldat.Vehicles.Api.Events;
using Teldat.Vehicles.Domain.IServices;

namespace Teldat.Vehicles.Api.Handlers
{
    public class SaveVehicleHandler : INotificationHandler<AddVehicleEvent>
    {
        private readonly IVehicleService vehicleService;
        private readonly IMediator mediator;

        public SaveVehicleHandler(IVehicleService vehicleService, IMediator mediator)
        {
            this.vehicleService = vehicleService;
            this.mediator = mediator;
        }

        public async Task Handle(AddVehicleEvent notification, CancellationToken cancellationToken)
        {
            await vehicleService.Add(notification.Vehicle);

            await mediator.Publish(new SavedVehicleEvent());

        }
    }
}
