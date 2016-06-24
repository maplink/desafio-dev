using System.Collections.Generic;
using System.Linq;

namespace Maplink.DesafioDev.Domain.Entities
{
    public class RouteResponse
    {
        public IEnumerable<RouteResponseItem> Data { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public bool Success => !Errors.Any() && Data != null && Data.Any();

        public RouteResponse(RouteResponseItem item)
        {
            Data = new[] {item};
        }

        public RouteResponse(IEnumerable<string> errors)
        {
            Errors = errors;
        }
    }
}