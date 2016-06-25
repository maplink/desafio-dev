using System;
using System.Threading.Tasks;
using Maplink.DesafioDev.Domain.Entities;

namespace Maplink.DesafioDev.Infrastructure.Services
{
    public class SearchService
    {
        private readonly string _apiSearchUrl;
        private readonly string _applicationCode;
        private readonly MaplinkService _maplinkService;

        public SearchService(string apiSearchUrl, string applicationCode, MaplinkService maplinkService)
        {
            _apiSearchUrl = apiSearchUrl;
            _applicationCode = applicationCode;
            _maplinkService = maplinkService;
        }

        protected SearchService()
        {
        }

        public virtual async Task<Location> GetLocation(Address address)
        {
            var requestUri = GetRequestUri(address);
            var searchResponse = await _maplinkService.GetContent(requestUri);
            var location = CreateResult(searchResponse);
            return location;
        }

        protected virtual string Sign(string uri)
        {
            var signedUri = _maplinkService.Sign(uri);
            return signedUri;
        }

        private static Location CreateResult(dynamic searchResponse)
        {
            dynamic location = searchResponse.results?[0]?.location;

            return new Location
            {
                Latitude = location?.lat,
                Longitude = location?.lng
            };
        }

        private string GetRequestUri(Address address)
        {
            var baseUri = new Uri(_apiSearchUrl);
            var uriParameters = $"?q={address.ToAutocomplete()}&applicationCode={_applicationCode}";
            var uri = new Uri(baseUri, uriParameters);
            
            var signedUri = Sign(uri.ToString());
            return signedUri;
        }
    }
}