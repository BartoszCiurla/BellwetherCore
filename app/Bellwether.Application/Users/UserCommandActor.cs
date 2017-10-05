using System;
using System.Threading.Tasks;
using Core.Akka.ActorAutostart;
using Core.Application.Actors;

using Bellwether.Application.Api.Common;
using Bellwether.Application.Api.Users;
using Bellwether.Domain.Users.Entities;
using Bellwether.Domain.Users;

namespace Bellwether.Application.Users
{
  [AutostartActor(DispatcherActorsNames.UserCommandActor)]
  public class UserCommandActor : BaseActor
  {
    private readonly IPasswordCryptoService _passwordCryptoService;

    public UserCommandActor(IActorBootstraper actorBootstraper,
                            IPasswordCryptoService passwordCryptoService) : base(actorBootstraper)
    {
      _passwordCryptoService = passwordCryptoService;

      ReceiveAsync<RegisterUserCommand>(Handle);
    }

    private async Task Handle(RegisterUserCommand command)
    {
      await HandleCommand(command, async uow =>
      {
        var userRepository = uow.GetRepository<BellwetherUser>();

        var salt = _passwordCryptoService.GenerateSalt();
        var passwordHash = _passwordCryptoService.HashPassword(command.Password, salt);

        var user = BellwetherUser.Create(Guid.NewGuid(), command.Name, command.Surname, command.Email, passwordHash, salt, UserType.Client);

        userRepository.Add(user);

        await userRepository.SaveChangesAsync();
      });
    }
  }
}
