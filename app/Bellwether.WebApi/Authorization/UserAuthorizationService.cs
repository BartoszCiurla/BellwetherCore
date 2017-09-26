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

namespace Bellwether.WebApi.Authorization
{
  public interface IUserAuthorizationService
  {
    Task<BellwetherUser> FindByEmailAsync(StringValues userEmail);
    Task<IEnumerable<string>> GetRolesAsync(BellwetherUser user);
    Task<bool> CheckPasswordAsync(BellwetherUser user, StringValues password);
  }

  public class UserAuthorizationService : IUserAuthorizationService
  {
    private readonly IReadOnlyUnitOfWork _readOnlyUnitOfWork;
    private readonly IPasswordProvider _passwordProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserAuthorizationService> _logger;

    public UserAuthorizationService(
        IReadOnlyUnitOfWork readOnlyUnitOfWork,
        IUnitOfWork unitOfWork,
        IPasswordProvider passwordProvider,
        ILogger<UserAuthorizationService> logger
        )
    {
      _readOnlyUnitOfWork = readOnlyUnitOfWork;
      _passwordProvider = passwordProvider;
      _unitOfWork = unitOfWork;
      _logger = logger;
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

    public async Task<IEnumerable<string>> GetRolesAsync(BellwetherUser user)
    {
      var userRepository = _readOnlyUnitOfWork.GetRepository<BellwetherUser>();
      var roleRepository = _readOnlyUnitOfWork.GetRepository<Role>();
      var ueserRecord = await userRepository.Query().FirstOrDefaultAsync(x => x.Id == user.Id);
      var roleIds = ueserRecord.UserRoles.Select(x => x.Role.Id).ToArray();

      var roles = await roleRepository.Query().Where(x => roleIds.Any(y => y == x.Id)).ToArrayAsync();

      var types = new List<string>();
      foreach (var item in roles)
      {
        types.Add(item.Type);
      }
      return types;
    }

    public async Task<bool> CheckPasswordAsync(BellwetherUser user, StringValues password)
    {
      var result = false;
      if (password.Count >= 1)
      {
        result = await _passwordProvider.IsCorrectAsync(password[0], user.Password, user.Salt);
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
