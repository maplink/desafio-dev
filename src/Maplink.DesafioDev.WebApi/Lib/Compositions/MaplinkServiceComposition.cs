using System.Configuration;
using LightInject;
using Maplink.DesafioDev.Infrastructure;
using Maplink.DesafioDev.Infrastructure.Services;

namespace Maplink.DesafioDev.WebApi.Lib.Compositions
{
    public class MaplinkServiceComposition : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register(factory => new SearchService(
                ConfigurationManager.AppSettings["MaplinkApi.Authentication.ApplicationCode"],
                ConfigurationManager.AppSettings["MaplinkApi.Search.Url"], 
                factory.GetInstance<MaplinkSignedUrl>()));
        }
    }
}