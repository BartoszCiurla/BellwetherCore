using System.Threading.Tasks;
using Bellwether.Application.Api.Common;
using Bellwether.Application.Api.Languages.Commands;
using Core.Akka.ActorAutostart;
using Core.Application.Actors;

namespace Bellwether.Application.Languages
{
  [AutostartActor(DispatcherActorsNames.LanguageCommandActor)]
  public class LanguageCommandActor : BaseActor
  {
    public LanguageCommandActor(IActorBootstraper actorBootstraper) : base(actorBootstraper)
    {
      ReceiveAsync<CreateLanguageCommand>(Handle);
    }

    private async Task Handle(CreateLanguageCommand command)
    {
      await HandleCommand(command, async uow =>
      {

      });
    }
  }
}
