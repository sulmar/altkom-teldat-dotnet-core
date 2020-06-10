using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoldiersController : ControllerBase
    {
        private readonly ISoldierService soldierService;

        public SoldiersController(ISoldierService soldierService)
        {
            this.soldierService = soldierService;
        }

        // POST https://localhost:5000/api/soldiers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Post([FromBody] Soldier soldier)
        {
            await soldierService.Add(soldier);

            return CreatedAtRoute("GetById", new { Id = soldier.Id }, soldier);
        }


    }
}
