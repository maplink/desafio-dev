using Maplink.DesafioDev.Domain.Entities;

namespace Maplink.DesafioDev.Commands
{
    public class DoRouteCommand
    {
        public virtual RouteResponse Execute(RouteRequest request)
        {
            return new RouteResponse();
        }
    }
}