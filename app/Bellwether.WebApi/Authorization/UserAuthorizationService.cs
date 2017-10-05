using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;

using Bellwether.Domain.Users.Entities;
using Core.Presentation.Reader;
using Core.Domain.Ddd;
using Microsoft.EntityFrameworkCore;
using Bellwether.Application.Users;
using Bellwether.Domain.Users;

namespace Bellwether.WebApi.Authorization
{
  public interface IUserAuthorizationService
  {
    Task<BellwetherUser> FindByEmailAsync(StringValues userEmail);
    Task<bool> CheckPasswordAsync(BellwetherUser user, StringValues password);
  }

  public class UserAuthorizationService : IUserAuthorizationService
  {
    private readonly IReadOnlyUnitOfWork _readOnlyUnitOfWork;
    private readonly IPasswordCryptoService _passwordCryptoService;
    private readonly IUnitOfWork _unitOfWork;

    public UserAuthorizationService(
        IReadOnlyUnitOfWork readOnlyUnitOfWork,
        IUnitOfWork unitOfWork,
        IPasswordCryptoService passwordCryptoService
        )
    {
      _readOnlyUnitOfWork = readOnlyUnitOfWork;
      _passwordCryptoService = passwordCryptoService;
      _unitOfWork = unitOfWork;
    }

    public async Task<BellwetherUser> FindByEmailAsync(StringValues userEmail)
    {
      BellwetherUser result = null;
      if (userEmail.Count >= 1)
      {
        result = await _readOnlyUnitOfWork.GetRepository<BellwetherUser>().Query().FirstOrDefaultAsync(x => x.Email.ToLower() == userEmail[0].ToLower());
      }
      return result;
    }
    public async Task<bool> CheckPasswordAsync(BellwetherUser user, StringValues password)
    {
      var result = false;
      if (password.Count >= 1)
      {
        result = await _passwordCryptoService.IsCorrectAsync(password[0], user.Password, user.Salt);
      }

      if (result)
      {
        user.UpdateLastLoginDate();
        await _unitOfWork.GetRepository<BellwetherUser>().SaveChangesAsync();
      }
      return result;
    }
  }
}
