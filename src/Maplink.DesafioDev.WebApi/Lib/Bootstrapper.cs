using LightInject;
using LightInject.Nancy;
using Maplink.DesafioDev.WebApi.Lib.Compositions;
using Nancy.Bootstrapper;
using Nancy.Json;
using Newtonsoft.Json;

namespace Maplink.DesafioDev.WebApi.Lib
{
    public class Bootstrapper : LightInjectNancyBootstrapper
    {
        public IServiceContainer Container => ApplicationContainer;

        protected override void ApplicationStartup(IServiceContainer serviceContainer, IPipelines pipelines)
        {
            JsonSettings.MaxJsonLength = int.MaxValue;

            base.ApplicationStartup(serviceContainer, pipelines);
            ConfigureApplicationContainer(serviceContainer);
        }

        protected override void ConfigureApplicationContainer(IServiceContainer container)
        {
            container.Register(typeof(JsonSerializer), typeof(CustomJsonSerializer));
            container.RegisterFrom<BaseComposition>();
            container.RegisterFrom<MaplinkServiceComposition>();
        }
    }
}