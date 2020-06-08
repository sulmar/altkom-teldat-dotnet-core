using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using Teldat.Vehicles.Domain.Models;

namespace Teldat.Vehicles.Infrastructure.Fakers
{

    // dotnet add package Bogus
    public class VehicleFaker : Faker<Vehicle>
    {
        public VehicleFaker()
        {
            UseSeed(1);
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Model, f => f.Vehicle.Model());
            RuleFor(p => p.Vin, f => f.Vehicle.Vin());
            RuleFor(p => p.ProductionYear, f => f.Random.Short(1950, 2020));
            RuleFor(p => p.IsRemoved, f => f.Random.Bool(0.2f));
        }
    }
}