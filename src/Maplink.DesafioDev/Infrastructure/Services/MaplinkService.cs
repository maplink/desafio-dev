
using System.Threading.Tasks;

namespace Maplink.DesafioDev.Infrastructure.Services
{
    public class MaplinkService
    {
        private readonly string _token;
        private readonly MaplinkSignUrl _maplinkSignUrl;
        private readonly RestHandler _restHandler;

        public MaplinkService(
            string token, 
            MaplinkSignUrl maplinkSignUrl,
            RestHandler restHandler)
        {
            _token = token;
            _maplinkSignUrl = maplinkSignUrl;
            _restHandler = restHandler;
        }

        protected MaplinkService()
        {
        }

        public virtual Task<dynamic> GetContent(string requestUri)
        {
            var content = _restHandler.Get(requestUri);
            return content;
        }

        public virtual string Sign(string url)
        {
            var signedUri = _maplinkSignUrl.Sign(url, _token);
            return signedUri;
        }
    }
}