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
                ConfigurationManager.AppSettings["MaplinkApi.Search.Url"],
                ConfigurationManager.AppSettings["MaplinkApi.Authentication.ApplicationCode"],
                factory.GetInstance<MaplinkService>()));

            serviceRegistry.Register(factory => new RouteService(
                ConfigurationManager.AppSettings["MaplinkApi.Route.Url"],
                ConfigurationManager.AppSettings["MaplinkApi.Authentication.ApplicationCode"],
                factory.GetInstance<MaplinkService>()));

            serviceRegistry.Register(factory => new MaplinkService(
                ConfigurationManager.AppSettings["MaplinkApi.Authentication.Token"],
                factory.GetInstance<MaplinkSignUrl>(),
                factory.GetInstance<RestHandler>()));
        }
    }
}