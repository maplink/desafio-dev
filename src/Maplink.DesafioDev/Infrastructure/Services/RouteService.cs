using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maplink.DesafioDev.Domain.Entities;

namespace Maplink.DesafioDev.Infrastructure.Services
{
    public class RouteService
    {
        private readonly string _apiRouteUrl;
        private readonly string _applicationCode;
        private readonly MaplinkService _maplinkService;

        public RouteService(string apiRouteUrl, string applicationCode, MaplinkService maplinkService)
        {
            _apiRouteUrl = apiRouteUrl;
            _applicationCode = applicationCode;
            _maplinkService = maplinkService;
        }

        public virtual async Task<RouteResponse> GetRouteData(IEnumerable<Location> locations, string routeType)
        {
            var requestUri = GetRequestUri(locations.ToList(), routeType);
            var routeResponse = await _maplinkService.GetContent(requestUri);
            var result = CreateResult(routeResponse);
            return new RouteResponse(result);
        }

        private static RouteResponseItem CreateResult(dynamic routeResponse)
        {
            dynamic route = routeResponse.routes?[0];

            return new RouteResponseItem
            {
                FuelCost = 0,
                TotalCostWithToll = 0,
                TotalDistance = route?.summary?.distance,
                TotalTime = route?.summary?.duration
            };
        }

        private string GetRequestUri(IList<Location> locations, string routeType)
        {
            var baseUri = new Uri(_apiRouteUrl);

            var uriParameters = new StringBuilder("?");

            for (var index = 0; index < locations.Count; index++)
            {
                uriParameters.Append($"{locations[index].ToWaypoint(index)}&");
            }

            uriParameters
                .Append($"travel.mode={routeType}")
                .Append($"&applicationCode={_applicationCode}");

            var uri = new Uri(baseUri, uriParameters.ToString());

            var signedUri = _maplinkService.Sign(uri.ToString());
            return signedUri;
        }
    }
}