using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Bellwether.WebApi.Authorization
{
  public class JwtOptions
  {
    public string Issuer { get; set; }
    public string Subject { get; set; }
    public string Audience { get; set; }
    public string Path { get; set; }
    public string TokenName { get; set; }
    public DateTime NotBefore { get; set; } = DateTime.Now;
    public DateTime IssuedAt { get; set; } = DateTime.Now;
    public TimeSpan ValidFor { get; set; }
    public DateTime Expiration => IssuedAt.Add(ValidFor);
    public Func<Task<string>> JtiGenerator =>
      () => Task.FromResult(Guid.NewGuid().ToString());
    public SigningCredentials SigningCredentials { get; set; }
  }
}
