using System.Collections.Generic;
using System.Reflection;
using Akka.Actor;

namespace Core.Akka.ActorAutostart
{
    public interface IAutostartActorInitializer
    {
        List<IActorRef> AutostartedActors { get; }
        void FindAndStartActors(params Assembly[] assembliesToScan);
        void StopAllAutostartedActors();
    }
}
