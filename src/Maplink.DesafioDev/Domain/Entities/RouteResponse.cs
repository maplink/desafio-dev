namespace Maplink.DesafioDev.Domain.Entities
{
    public class RouteResponse
    {
        public int TotalTime { get; set; }
        public int TotalDistance { get; set; }
        public int FuelCost { get; set; }
        public int TotalCostWithToll { get; set; }
    }
}