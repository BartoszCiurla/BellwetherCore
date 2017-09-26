using Akka.Configuration;

namespace Core.Akka.ActorSystem
{
  public class ActorSystemManager : IActorSystemManager
  {
    public ActorSystemManager(string name, Config config = null)
    {
      Name = name;
      BasePath = $"akka://{name}/user/";
      ActorSystem = config == null
                        ? global::Akka.Actor.ActorSystem.Create(name)
                        : global::Akka.Actor.ActorSystem.Create(name, config);
    }
    public string BasePath { get; }
    public string Name { get; }

    public global::Akka.Actor.ActorSystem ActorSystem { get; }

    public string FullPathForActor(string actorPath) => $"{BasePath}{actorPath}";

    public void Dispose()
    {
      ActorSystem?.Dispose();
    }
  }
}
