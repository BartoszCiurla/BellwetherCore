using Autofac;

using Core.Domain;
using Core.Infrastructure;
using Core.Presentation;
using Bellwether.Infrastructure;

namespace Bellwether.WebApi
{
    public class BellwetherWebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CoreInfrastructureModule>();
            builder.RegisterModule<CoreDomainModule>();
            builder.RegisterModule<CorePresentationModule>();
            builder.RegisterModule<BellwetherInfrastructureModule>();

            builder.RegisterAssemblyTypes(ThisAssembly)
                   .AsImplementedInterfaces()
                   .PreserveExistingDefaults();
        }
    }
}
