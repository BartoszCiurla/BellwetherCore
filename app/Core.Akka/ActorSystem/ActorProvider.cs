using System;
using System.Threading.Tasks;
using Akka.Actor;

namespace Core.Akka.ActorSystem
{
  public class ActorProvider : IActorProvider
  {
    private readonly IActorSystemManager _actorSystemManager;

    public ActorProvider(IActorSystemManager actorSystemManager)
    {
      _actorSystemManager = actorSystemManager;
    }

    public async Task<IActorRef> GetOne(string actorName)
    {
      var path = _actorSystemManager.FullPathForActor(actorName);
      return await _actorSystemManager.ActorSystem.ActorSelection(path).ResolveOne(TimeSpan.FromSeconds(30));
    }
  }
}
