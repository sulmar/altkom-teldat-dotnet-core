using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Teldat.Vehicles.Domain.IServices;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.RazorPages.Pages.Vehicles
{
    public class IndexModel : PageModel
    {
        private readonly IVehicleService vehicleService;

        public IEnumerable<Vehicle> Vehicles { get; set; }

        public IndexModel(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        public async Task OnGet()
        {
            Vehicles = await vehicleService.Get();
        }

        public async Task OnPost()
        {

        }
    }
}