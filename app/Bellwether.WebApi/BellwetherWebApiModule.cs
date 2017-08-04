using Autofac;

using Core.Domain;
using Core.Infrastructure;
using Core.Presentation;

namespace Bellwether.WebApi
{
    public class BellwetherWebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CoreInfrastructureModule>();
            builder.RegisterModule<CoreDomainModule>();
            builder.RegisterModule<CorePresentationModule>();

            builder.RegisterAssemblyTypes(ThisAssembly)
                   .AsImplementedInterfaces()
                   .PreserveExistingDefaults();
        }
    }
}
