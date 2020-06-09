using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Teldat.Vehicles.Api.Events;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Api.Handlers
{
    public class GetVehicleRequestHandler : IRequestHandler<GetVehicleRequest, Vehicle>
    {
        private readonly IVehicleService vehicleService;

        public GetVehicleRequestHandler(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public Task<Vehicle> Handle(GetVehicleRequest request, CancellationToken cancellationToken)
        {
            return vehicleService.Get(request.Id);
        }
    }
}
