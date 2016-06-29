using Maplink.DesafioDev.Commands;
using Maplink.DesafioDev.Domain.Entities;
using Nancy;

namespace Maplink.DesafioDev.WebApi.Modules
{
    public class RouteModule : BaseModule
    {
        public RouteModule(DoRouteCommand command) : base("routes")
        {
            Post["/", true] = async (parameters, ct) =>
            {
                var request = BindRequest<RouteRequest>();
                var response = await command.Execute(request);
                return Response.AsJson(response);
            };
        }
    }
}