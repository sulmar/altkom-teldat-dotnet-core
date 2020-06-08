using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Api.Controllers
{
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        public VehiclesController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        // GET https://localhost:5000/api/vehicles
        //[HttpGet]
        //public Task<IEnumerable<Vehicle>> Get()
        //{
        //    return vehicleService.Get();
        //}

        // GET https://localhost:5000/api/vehicles
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var vehicles = await vehicleService.Get();
            return Ok(vehicles);
        }

        // GET https://localhost:5000/api/vehicles/10
        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> Get(int id)
        {
            Vehicle vehicle = await vehicleService.Get(id);

            if(vehicle==null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        // GET https://localhost:5000/api/vehicles/10/members
        [HttpGet("{vehicleId}/members")]
        public async Task<IActionResult> GetMembers(int vehicleId)
        {
            return Ok();
        }

        // POST https://localhost:5000/api/vehicles
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Vehicle vehicle)
        {
            await vehicleService.Add(vehicle);

            return CreatedAtRoute("GetById", new { Id = vehicle.Id }, vehicle);
        }

        // PUT https://localhost:5000/api/vehicles/10
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.Id)
                return BadRequest();

            await vehicleService.Update(vehicle);

            return NoContent();
        }

        // DELETE https://localhost:5000/api/vehicles/10
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Vehicle vehicle = await vehicleService.Get(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            await vehicleService.Remove(id);

            return NoContent();
        }
    }
}
