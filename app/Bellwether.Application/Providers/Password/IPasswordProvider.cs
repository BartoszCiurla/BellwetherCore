using System.Threading.Tasks;

namespace Bellwether.Application.Providers.Password
{
  public interface IPasswordProvider
  {
    Task<bool> IsCorrectAsync(string password, string passwordHash, string salt);
    string GenerateSalt();
    string HashPassword(string password, string salt);
  }
}
