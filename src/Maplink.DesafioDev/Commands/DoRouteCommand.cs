using System.Linq;
using System.Threading.Tasks;
using Maplink.DesafioDev.Domain.Entities;
using Maplink.DesafioDev.Infrastructure.Services;

namespace Maplink.DesafioDev.Commands
{
    public class DoRouteCommand
    {
        private readonly SearchService _searchService;
        private readonly RouteService _routeService;

        public DoRouteCommand(SearchService searchService, RouteService routeService)
        {
            _searchService = searchService;
            _routeService = routeService;
        }

        public virtual async Task<RouteResponse> Execute(RouteRequest request)
        {
            var locations = await Task.WhenAll(request
                .Addresses
                .Select(p => _searchService.GetLocation(p))
                .ToList());

            var routeResponse = _routeService.GetRouteData(locations, request.Type);
            return await routeResponse;
        }
    }
}