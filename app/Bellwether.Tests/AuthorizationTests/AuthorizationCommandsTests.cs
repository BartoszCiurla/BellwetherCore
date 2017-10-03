using Autofac;
using Bellwether.Domain.Users;
using Bellwether.Tests.Fixtures;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Bellwether.Tests.AuthorizationTests
{
  [Collection("Autofac")]
  public class PasswordCryptoServiceTests
  {
    private readonly ITestOutputHelper _outputHelper;
    private readonly IPasswordCryptoService _passwordCryptoService;

    public PasswordCryptoServiceTests(AutofacFixture autofac, ITestOutputHelper outputHelper)
    {
      _outputHelper = outputHelper;
      _passwordCryptoService = autofac.Scope.Resolve<IPasswordCryptoService>();
    }

    [Theory]
    [InlineData("Pa$$word123")]
    [InlineData("pa&&worD123")]
    public void it_generates_password_hash(string password)
    {
      var salt = _passwordCryptoService.GenerateSalt();
      var passwordHash = _passwordCryptoService.HashPassword(password, salt);
      _outputHelper.WriteLine($"Password hash: {passwordHash}");

      var result = _passwordCryptoService.IsCorrectAsync(password, passwordHash, salt).Result;

      result.Should().BeTrue();
    }
  }
}
