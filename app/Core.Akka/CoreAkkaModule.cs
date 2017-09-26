using Autofac;
using Core.Akka.ActorSystem;

namespace Core.Akka
{
    public class CoreAkkaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                   .AsImplementedInterfaces()
                   .PreserveExistingDefaults();

            builder.Register(ctx => new ActorSystemManager("ActorSystem")).AsImplementedInterfaces().SingleInstance();
        }
    }
}
