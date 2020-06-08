namespace Teldat.Vehicles.Domain.Models
{
    public class Vehicle : Base
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public short ProductionYear { get; set; }
        public bool IsRemoved { get; set; }
    }
}
