using System.Threading.Tasks;
using Bellwether.Application.Api.Messages.Responses;
using Bellwether.Application.Api.Users;

namespace Bellwether.Application.Users
{
  public interface IUserService
  {
    Task RegisterUser(RegisterUserCommand command);
  }
}
