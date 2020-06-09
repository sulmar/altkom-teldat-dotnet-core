using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Teldat.Vehicles.Api.Constraints;
using Teldat.Vehicles.Api.Events;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Api.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        private readonly IMediator mediator;

        public VehiclesController(IVehicleService vehicleService, IMediator mediator)
        {
            this.vehicleService = vehicleService;
            this.mediator = mediator;
        }

        // GET https://localhost:5000/api/vehicles
        [HttpGet]
        public Task<IEnumerable<Vehicle>> Get()
        {
            return vehicleService.Get();
        }

        // GET https://localhost:5000/api/vehicles
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var vehicles = await vehicleService.Get();
        //    return Ok(vehicles);
        //}

        // GET https://localhost:5000/api/vehicles/10
        //[HttpGet("{id}", Name = "GetById")]
        //[ProducesResponseType(typeof(Vehicle), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> Get(int id)
        //{
        //    Vehicle vehicle = await vehicleService.Get(id);

        //    if(vehicle==null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(vehicle);
        //}

        // GET https://localhost:5000/api/vehicles/10
        //[HttpGet("{id:int}", Name = "GetById")]
        [HttpGet("{id:int}.{format?}", Name = "GetById")]
        [FormatFilter]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Vehicle>> Get([FromRoute] GetVehicleRequest request)
        {
            
            // Vehicle vehicle = await vehicleService.Get(id);

            Vehicle vehicle = await mediator.Send(request);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        [HttpGet("{vin}")]
        public async Task<ActionResult<Vehicle>> Get(string vin)
        {
            Vehicle vehicle = await vehicleService.Get(vin);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        // GET https://localhost:5000/api/vehicles/10/members
        [HttpGet("{vehicleId}/members")]
        //[HttpGet]
        //[Route("{vehicleId}/members")]
        public async Task<IActionResult> GetMembers(int vehicleId)
        {
            return Ok();
        }


        // GET https://localhost:5000/api/vehicles?lat=58.43&lng=21.05&range=100


        //[HttpGet]
        //public async Task<IActionResult> Get(
        //    [RequiredFromQuery] double lat,
        //    [RequiredFromQuery] double lng,
        //    double range = 100)
        //{
        //    return Ok();
        //}

        //[HttpGet]
        //public async Task<IActionResult> Get(
        //    [Required][Range(-90, 90)] double lat,
        //    [Required][Range(-180, 180)] double lng, double range = 100)
        //{
        //    return Ok();
        //}

        // GET https://localhost:5000/api/vehicles?lat=58.43&lng=21.05&range=100

        //[HttpGet]
        //public async Task<IActionResult> Get([RequiredFromQuery] Location location)
        //{
        //    return Ok();
        //}


        // POST https://localhost:5000/api/vehicles
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post(
           [FromBody] Vehicle vehicle, 
           [FromServices] IMessageSender messageSender )
        {
            // realizowane przez [ApiController]
            //if (!this.ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}    

            // dotnet add package MediatR

            //await vehicleService.Add(vehicle);
            //await messageSender.SendAsync($"Vehicle {vehicle.Model} was added");            

            // await mediator.Publish(new AddVehicleEvent(vehicle));

            bool result = await mediator.Send(new SaveVehicleEvent(vehicle));

            return CreatedAtRoute("GetById", new { Id = vehicle.Id }, vehicle);
        }

        // HEAD https://localhost:5000/api/vehicles/10
        [HttpHead("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Exists(int id)
        {
            Vehicle vehicle = await vehicleService.Get(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok();
        }


        // Json Patch http://jsonpatch.com/
        // [AcceptVerbs("Get, Head")]
        // [AcceptVerbs("Patch, Post")]
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] Vehicle vehicle)
        {
            throw new NotImplementedException();
        }

        // PUT https://localhost:5000/api/vehicles/10
        [HttpPut("{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]        
        public async Task<IActionResult> Put(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.Id)
                return BadRequest();

            await vehicleService.Update(vehicle);

            return NoContent();
        }

        // DELETE https://localhost:5000/api/vehicles/10
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id, [FromServices] IHttpClientFactory httpClientFactory)
        {
            HttpClient client = httpClientFactory.CreateClient("GitHub");            

            Vehicle vehicle = await vehicleService.Get(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            await vehicleService.Remove(id);

            return NoContent();
        }

        // POST https://localhost:5000/api/vehicles/upload
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upload(IList<IFormFile> files)
        {
            foreach (var file in files)
            {
                // TODO: process file
            }

            long size = files.Sum(f => f.Length);

            return Accepted(new { count = files.Count, size });

        }
    }
}
