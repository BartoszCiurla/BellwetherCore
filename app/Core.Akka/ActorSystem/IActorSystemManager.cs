using System;

namespace Core.Akka.ActorSystem
{
  public interface IActorSystemManager : IDisposable
  {
    global::Akka.Actor.ActorSystem ActorSystem { get; }
    string Name { get; }
    string BasePath { get; }
    string FullPathForActor(string actorPath);
  }
}
