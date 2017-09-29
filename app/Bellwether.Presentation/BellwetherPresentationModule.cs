using Akka.Actor;
using Autofac;
using Core.Presentation.Actors;

namespace Bellwether.Presentation
{
    public class BellwetherPresentationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                   .Where(t => t.IsAssignableTo<ReceiveActor>())
                   .AsSelf();

            builder.RegisterAssemblyTypes(ThisAssembly)
                   .AsImplementedInterfaces()
                   .PreserveExistingDefaults();
        }
    }
}
