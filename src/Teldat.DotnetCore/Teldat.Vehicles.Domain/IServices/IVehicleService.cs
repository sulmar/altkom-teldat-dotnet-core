using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Domain.IServices
{
    public interface IEntityService<TEntity>
        where TEntity : Base
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> Get(int id);
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Remove(int id);
    }

    public interface ISoldierService : IEntityService<Soldier>
    {

    }

    public interface ICivilService : IEntityService<Civil>
    {

    }

    public interface IVehicleService : IEntityService<Vehicle>
    {
        Task<Vehicle> Get(string vin);
    }
}
