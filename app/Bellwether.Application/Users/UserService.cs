using System;
using System.Threading.Tasks;

using Bellwether.Application.Api.Users;
using Bellwether.Application.Providers.Password;
using Bellwether.Domain.Users.Entities;
using Core.Domain.Ddd;

namespace Bellwether.Application.Users
{
  public class UserService : IUserService
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordProvider _passwordProvider;

    public UserService(IUnitOfWork unitOfWork, IPasswordProvider passwordProvider)
    {
      _unitOfWork = unitOfWork;
      _passwordProvider = passwordProvider;
    }

    public async Task RegisterUser(RegisterUserCommand command)
    {
      var repo = _unitOfWork.GetRepository<BellwetherUser>();
      var salt = _passwordProvider.GenerateSalt();
      var passwordHash = _passwordProvider.HashPassword(command.Password, salt);
      var user = BellwetherUser.Create(Guid.NewGuid(), command.Name, command.Surname, command.Email, passwordHash, salt, null);
      repo.Add(user);
      await repo.SaveChangesAsync();
    }
  }
}
