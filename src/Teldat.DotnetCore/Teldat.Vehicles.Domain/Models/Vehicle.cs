using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Teldat.Vehicles.Domain.Models
{
    public class Vehicle : Base
    {        
        public int Id { get; set; }

        // shadow property
        // public int DriverId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]        
        public string Model { get; set; }
        public string Vin { get; set; }
        public Soldier Driver { get; set; }
        public IEnumerable<Person> Passengers { get; set; }

        [Range(1950, 2100)]
        public short ProductionYear { get; set; }
        public bool IsRemoved { get; set; }
    }

    public class Civil : Person
    {
        public decimal Salary { get; set; }
    }

    public class Soldier : Person
    {
        public string Unit { get; set; }
        public Ranks Rank { get; set; }
    }

    public enum Ranks
    {
        Private,
        Officer
    }


    public abstract class Person : Base
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
