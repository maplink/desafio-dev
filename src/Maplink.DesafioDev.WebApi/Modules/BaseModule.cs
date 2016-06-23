using Maplink.DesafioDev.Domain.Entities;
using Maplink.DesafioDev.Infrastructure.Extensions;
using Maplink.DesafioDev.WebApi.Lib.Handlers;
using Nancy;
using Nancy.ModelBinding;

namespace Maplink.DesafioDev.WebApi.Modules
{
    public abstract class BaseModule : NancyModule
    {
        protected BaseModule(string modulePath) : base(modulePath)
        {
            OnError += new ErrorHandler().OnError;
        }

        protected T BindRequest<T>() where T : IEntityValidation
        {
            var filter = this.Bind<T>();

            filter
                .Validate()
                .ThrowOnFailure();

            return filter;
        }
    }
}