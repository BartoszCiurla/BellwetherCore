using System.Threading.Tasks;
using Akka.Actor;

namespace Core.Akka.ActorSystem
{
    public interface IActorProvider
    {
        Task<IActorRef> GetOne(string actorName);
    }
}
