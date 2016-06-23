namespace Maplink.DesafioDev.Domain.Entities
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual string ToWaypoint(int index)
        {
            return $"waypoint.{index}.latlng={Longitude},{Latitude}";
        }
    }
}