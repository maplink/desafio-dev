
using System.Threading.Tasks;

namespace Maplink.DesafioDev.Infrastructure.Services
{
    public class MaplinkService
    {
        private readonly string _token;
        private readonly MaplinkSignedUrl _maplinkSignedUrl;
        private readonly RestHandler _restHandler;

        public MaplinkService(
            string token, 
            MaplinkSignedUrl maplinkSignedUrl,
            RestHandler restHandler)
        {
            _token = token;
            _maplinkSignedUrl = maplinkSignedUrl;
            _restHandler = restHandler;
        }

        public virtual Task<dynamic> GetContent(string requestUri)
        {
            var content = _restHandler.Get(requestUri);
            return content;
        }

        public string Sign(string url)
        {
            var signedUri = _maplinkSignedUrl.Sign(url, _token);
            return signedUri;
        }
    }
}