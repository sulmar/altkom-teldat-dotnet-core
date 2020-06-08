using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Teldat.Vehicles.Domain.Models
{
    public class Vehicle : Base
    {        
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]        
        public string Model { get; set; }
        public string Vin { get; set; }

        [Range(1950, 2100)]
        public short ProductionYear { get; set; }
        public bool IsRemoved { get; set; }
    }
}
