using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Api.Events
{

    public class AddVehicleEvent : INotification  // interfejs wskazujacy (mark interface)
    {
        public AddVehicleEvent(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }

        public Vehicle Vehicle { get; private set; }
    }



   
}
