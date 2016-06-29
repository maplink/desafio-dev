using System.Globalization;

namespace Maplink.DesafioDev.Domain.Entities
{
    public class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public virtual string ToWaypoint(int index)
        {
            return $"waypoint.{index}.latlng={FormatLatLng(Latitude)},{FormatLatLng(Longitude)}";
        }

        private static string FormatLatLng(double value)
        {
            return value
                .ToString(CultureInfo.InvariantCulture)
                .Replace(",", ".");
        }
    }
}