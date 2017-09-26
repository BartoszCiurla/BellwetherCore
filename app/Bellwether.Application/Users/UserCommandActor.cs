using System;
using System.Threading.Tasks;
using Bellwether.Application.Api.Common;
using Bellwether.Application.Api.Users;
using Bellwether.Domain.Users.Entities;
using Core.Akka.ActorAutostart;
using Core.Application.Actors;

namespace Bellwether.Application.Users
{
  [AutostartActor(DispatcherActorsNames.UserCommandActor)]
  public class UserCommandActor : BaseActor
  {
    private readonly IPasswordProvider _passwordProvider;

    public UserCommandActor(IActorBootstraper actorBootstraper,
                            IPasswordProvider passwordProvider) : base(actorBootstraper)
    {
      _passwordProvider = passwordProvider;

      ReceiveAsync<RegisterUserCommand>(Handle);
    }

    private async Task Handle(RegisterUserCommand command)
    {
      await HandleCommand(command, async uow =>
      {
        var userRepository = uow.GetRepository<BellwetherUser>();

        var salt = _passwordProvider.GenerateSalt();
        var passwordHash = _passwordProvider.HashPassword(command.Password, salt);

        var user = BellwetherUser.Create(Guid.NewGuid(), command.Name, command.Surname, command.Email, passwordHash, salt, null);

        userRepository.Add(user);

        await userRepository.SaveChangesAsync();
      });
    }
  }
}
