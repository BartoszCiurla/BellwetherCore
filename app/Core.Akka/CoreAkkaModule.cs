using Akka.Configuration;
using Autofac;
using Core.Akka.ActorSystem;

namespace Core.Akka
{
  public class CoreAkkaModule : Module
  {
    private readonly Config AkkaConfig = ConfigurationFactory.ParseString(@"
                                                                        akka {
                                                                            suppress-json-serializer-warning = on
                                                                        }
                                                                        ");
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(ThisAssembly)
             .AsImplementedInterfaces()
             .PreserveExistingDefaults();

      builder.Register(ctx => new ActorSystemManager("ActorSystem", AkkaConfig)).AsImplementedInterfaces().SingleInstance();
    }
  }
}
