using System.IO;
using System;
using Akka.DI.AutoFac;
using Autofac;
using Bellwether.Application;
using Bellwether.Presentation;
using Core.Akka.ActorAutostart;
using Core.Akka.ActorSystem;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Bellwether.Tests.Fixtures
{
  public class AutofacFixture : IDisposable
  {
    private IAutostartActorInitializer _autostartActorInitializer;
    private IConfigurationRoot _configuration;
    public ILifetimeScope Scope { get; private set; }

    public AutofacFixture()
    {
      _configuration = LoadConfiguration();

      RebuildScopeWithRegistrations(builder =>
      {
      });
    }

    public void Dispose()
    {
      Scope?.Dispose();
    }

    private IConfigurationRoot LoadConfiguration()
    {
      var builder = new ConfigurationBuilder()
          //.SetBasePath(env.ContentRootPath)
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", true, true);

      return builder.Build();
    }

    public void RebuildScopeWithRegistrations(Action<ContainerBuilder> registrations)
    {
      _autostartActorInitializer?.StopAllAutostartedActors();
      _autostartActorInitializer = null;

      var builder = new ContainerBuilder();
      builder.RegisterModule<BellwetherTestsModule>();
      builder.RegisterInstance(_configuration).AsImplementedInterfaces();
      registrations(builder);
      Scope = builder.Build();

      new AutoFacDependencyResolver(Scope, Scope.Resolve<IActorSystemManager>().ActorSystem);

      _autostartActorInitializer = Scope.Resolve<IAutostartActorInitializer>();
      _autostartActorInitializer.FindAndStartActors(typeof(BellwetherApplicationModule).Assembly, typeof(BellwetherPresentationModule).Assembly);
    }
  }

  [CollectionDefinition("Autofac")]
  public class AutofacCollection : ICollectionFixture<AutofacFixture>
  {
      // This class has no code, and is never created. Its purpose is simply
      // to be the place to apply [CollectionDefinition] and all the
      // ICollectionFixture<> interfaces.
  }
}
