using System;
using Maplink.DesafioDev.Domain.Entities;

namespace Maplink.DesafioDev.Infrastructure.Services
{
    public class SearchService
    {
        private readonly string _applicationCode;
        private readonly string _apiSearchUrl;
        private readonly MaplinkSignedUrl _maplinkSignedUrl;

        public SearchService(string applicationCode,
            string apiSearchUrl,
            MaplinkSignedUrl maplinkSignedUrl)
        {
            _applicationCode = applicationCode;
            _apiSearchUrl = apiSearchUrl;
            _maplinkSignedUrl = maplinkSignedUrl;
        }

        public virtual Location GetLocation(Address address)
        {
            var requestUri = GetRequestUri(address);
            return new Location();
        }

        private string GetRequestUri(Address address)
        {
            var baseUri = new Uri(_apiSearchUrl);
            var uriParameters = $"?q={address.ToAutocomplete()}";
            var uri = new Uri(baseUri, uriParameters);
            var signedUri = _maplinkSignedUrl.Sign(uri.ToString(), _applicationCode);
            return signedUri;
        }
    }
}