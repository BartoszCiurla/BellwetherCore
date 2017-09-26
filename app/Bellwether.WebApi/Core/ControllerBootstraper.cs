using Autofac;
using Core.Akka.ActorSystem;

namespace Bellwether.WebApi.Core
{
  public class ControllerBootstraper
  {
    public IActorProvider ActorProvider { get; }
    public ILifetimeScope Scope { get; }

    public ControllerBootstraper(IActorProvider actorProvider, ILifetimeScope scope)
    {
      ActorProvider = actorProvider;
      Scope = scope;
    }
  }
}
