using Autofac;

using Core.Domain;
using Core.Infrastructure;
using Core.Presentation;
using Bellwether.Infrastructure;
using Bellwether.WebApi.Authorization;
using Bellwether.Application;
using Bellwether.WebApi.Core;
using Core.Akka;
using Core.Akka.ActorSystem;
using Core.Application;
using Bellwether.Presentation;

namespace Bellwether.WebApi
{
  public class BellwetherWebApiModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterModule<CoreInfrastructureModule>();
      builder.RegisterModule<CoreApplicationModule>();
      builder.RegisterModule<CoreDomainModule>();
      builder.RegisterModule<CorePresentationModule>();
      builder.RegisterModule<BellwetherPresentationModule>();
      builder.RegisterModule<BellwetherInfrastructureModule>();
      builder.RegisterModule<BellwetherApplicationModule>();
      builder.RegisterModule<CoreAkkaModule>();

      builder.RegisterAssemblyTypes(ThisAssembly)
             .AsImplementedInterfaces()
             .PreserveExistingDefaults();

      builder.RegisterType<ControllerBootstraper>().AsSelf();
      builder.RegisterType<JwtTokenGenerator>().SingleInstance();
      builder.RegisterType<JwtTokenMiddleware>().SingleInstance();
    }
  }
}
