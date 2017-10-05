using System;
using Bellwether.Domain.Users;
using Bellwether.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bellwether.Infrastructure.Ef.Extensions
{
  public static class DbContextExtensions
  {
    public static void EnsureSeedData(this BellwetherContext bellwetherContext, IPasswordCryptoService passwordCryptoService)
    {
      var bellwetherUsers = bellwetherContext.Set<BellwetherUser>();

      if (!bellwetherUsers.AnyAsync(bu => bu.UserType == UserType.Admin).Result)
      {
        SeedUser("Admin", "Admin", "admin@gmail.com", "admin1234", UserType.Admin, passwordCryptoService, bellwetherContext);
      }

      if (!bellwetherUsers.AnyAsync(bu => bu.UserType == UserType.Client).Result)
      {
        SeedUser("Clinet", "Client", "client@gmail.com", "client1234", UserType.Client, passwordCryptoService, bellwetherContext);
      }

      bellwetherContext.SaveChangesAsync();
    }

    private static async void SeedUser(string name,
                               string surname,
                               string email,
                               string password,
                               UserType userType,
                               IPasswordCryptoService passwordCryptoService,
                               BellwetherContext bellwetherContext)
    {
      var salt = passwordCryptoService.GenerateSalt();
      var passwordHash = passwordCryptoService.HashPassword(password, salt);
      var user = BellwetherUser.Create(Guid.NewGuid(), name, surname, email, passwordHash, salt, userType);
      await bellwetherContext.Set<BellwetherUser>().AddAsync(user);
    }
  }
}
