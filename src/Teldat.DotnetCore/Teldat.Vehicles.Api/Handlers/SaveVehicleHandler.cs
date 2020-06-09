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
    //public class SecondSaveVehicleRequestHandler : IRequestHandler<SaveVehicleEvent, bool>
    //{
    //    public async Task<bool> Handle(SaveVehicleEvent request, CancellationToken cancellationToken)
    //    {
    //        await Task.Delay(TimeSpan.FromSeconds(5));

    //        return false;
    //    }
    //}

    public class SaveVehicleRequestHandler : IRequestHandler<SaveVehicleEvent, bool>
    {
        private readonly IVehicleService vehicleService;

        public SaveVehicleRequestHandler(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public async Task<bool> Handle(SaveVehicleEvent request, CancellationToken cancellationToken)
        {
            await vehicleService.Add(request.Vehicle);

            return true;
        }
    }

    public class SaveVehicleHandler : INotificationHandler<AddVehicleEvent>
    {
        private readonly IVehicleService vehicleService;

        public SaveVehicleHandler(IVehicleService vehicleService, IMediator mediator)
        {
            this.vehicleService = vehicleService;
        }

        public async Task Handle(AddVehicleEvent notification, CancellationToken cancellationToken)
        {
            await vehicleService.Add(notification.Vehicle);

        }
    }
}
