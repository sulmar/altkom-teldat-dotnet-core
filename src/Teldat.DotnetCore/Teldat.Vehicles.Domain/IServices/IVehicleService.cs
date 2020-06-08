using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Domain.IServices
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> Get();
        Task<Vehicle> Get(int id);
        Task Add(Vehicle vehicle);
        Task Update(Vehicle vehicle);
        Task Remove(int id);
    }
}
