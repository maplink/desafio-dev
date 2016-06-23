using Maplink.DesafioDev.Commands;
using Maplink.DesafioDev.Domain.Entities;
using Nancy;

namespace Maplink.DesafioDev.WebApi.Modules
{
    public class RouteModule : BaseModule
    {
        public RouteModule(DoRouteCommand command) : base("routes")
        {
            Post["/"] = _ =>
            {
                var request = BindRequest<RouteRequest>();
                var response = command.Execute(request);
                return Response.AsJson(response);
            };
        }
    }
}