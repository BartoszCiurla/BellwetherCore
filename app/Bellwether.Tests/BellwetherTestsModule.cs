using Autofac;
using Bellwether.Application;
using Bellwether.Infrastructure;
using Bellwether.Presentation;
using Core.Akka;
using Core.Akka.ActorSystem;
using Core.Application;
using Core.Domain;
using Core.Infrastructure;
using Core.Presentation;

namespace Bellwether.Tests
{
  public class BellwetherTestsModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterModule<CoreInfrastructureModule>();
      builder.RegisterModule<CoreDomainModule>();
      builder.RegisterModule<CoreApplicationModule>();
      builder.RegisterModule<CorePresentationModule>();
      builder.RegisterModule<CoreAkkaModule>();

      builder.RegisterModule<BellwetherInfrastructureModule>();
      //todo domain module
      builder.RegisterModule<BellwetherApplicationModule>();
      builder.RegisterModule<BellwetherPresentationModule>();

      builder.Register(ctx => new ActorSystemManager("ActorSystem")).AsImplementedInterfaces().SingleInstance();
    }
  }
}
