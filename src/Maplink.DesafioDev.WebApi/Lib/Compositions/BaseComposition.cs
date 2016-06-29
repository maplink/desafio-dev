using System.Reflection;
using LightInject;

namespace Maplink.DesafioDev.WebApi.Lib.Compositions
{
    public class BaseComposition : ICompositionRoot
    {
        private static readonly Assembly CoreAssembly = Assembly.Load("Maplink.DesafioDev");

        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterAssembly(CoreAssembly);
        }
    }
}