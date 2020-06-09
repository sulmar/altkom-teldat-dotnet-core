using MediatR;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Api.Events
{
    // dotnet add package MediatR

    public class SaveVehicleEvent : IRequest<bool>
    {
        public SaveVehicleEvent(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }

        public Vehicle Vehicle { get; private set; }
    }


    public class GetVehicleRequest : IRequest<Vehicle>
    {
        public int Id { get; set; }
    }



   
}
