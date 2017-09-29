using System.Collections.Generic;
using System.Threading.Tasks;
using Bellwether.Application.Api;
using Bellwether.Application.Api.Common;
using Core.Akka.ActorAutostart;
using Core.Presentation.Actors;

namespace Bellwether.Presentation.Values
{
  [AutostartActor(DispatcherActorsNames.ValuesQueryActor)]
  public class ValueQueryActor : BaseActor
  {
    public ValueQueryActor(IActorBootstraper actorBootstraper) : base(actorBootstraper)
    {
      ReceiveAsync<GetValuesQuery>(Handle);
    }

    private async Task Handle(GetValuesQuery query)
    {
      await HandleQuery(query, uow =>
      {
        return new GetValuesResult{Values=new List<string>{"value1","value2"}};
      });
    }
  }
}
