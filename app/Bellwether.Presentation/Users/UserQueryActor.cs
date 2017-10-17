using System.Linq;
using System.Threading.Tasks;
using Bellwether.Application.Api.Common;
using Bellwether.Application.Api.Common.Dtos;
using Bellwether.Application.Api.Users.Queries;
using Bellwether.Domain.Users.Entities;
using Core.Akka.ActorAutostart;
using Core.Presentation.Actors;

namespace Bellwether.Presentation.Users
{
  [AutostartActor(DispatcherActorsNames.UserQueryActor)]
  public class UserQueryActor : BaseActor
  {
    public UserQueryActor(IActorBootstraper actorBootstraper) : base(actorBootstraper)
    {
      ReceiveAsync<GetUsersQuery>(Handle);
    }

    private async Task Handle(GetUsersQuery query)
    {
      await HandleQuery(query, (uow) =>
      {
        var userReadOnlyRepository = uow.GetRepository<BellwetherUser>();
        return new GetUsersResult(userReadOnlyRepository.Query().Select(uror => new UserDetailsDto(uror.Name,uror.Email)).ToList());
      });
    }
  }
}
