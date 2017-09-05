using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bellwether.Domain.Users.Entities;
using Bellwether.WebApi.Providers;
using Core.Domain.Ddd;
using Microsoft.AspNetCore.Mvc;

namespace Bellwether.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class AccountController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordProvider _passwordProvider;

    public AccountController(IUnitOfWork unitOfWork, IPasswordProvider passwordProvider)
    {
      _unitOfWork = unitOfWork;
      _passwordProvider = passwordProvider;
    }

    [HttpPost]
    public async Task<IActionResult> Register(JustTest model)
    {
      var repo = _unitOfWork.GetRepository<BellwetherUser>();
      var salt = _passwordProvider.GenerateSalt();
      var passwordHash = _passwordProvider.HashPassword(model.Password, salt);

      var user = BellwetherUser.Create(Guid.NewGuid(), model.Name, model.Surname, model.Email, passwordHash, salt, null);
      repo.Add(user);
      await repo.SaveChangesAsync();
      return Ok();
    }

    public class JustTest
    {
      public string Surname { get; set; }
      public string Name { get; set; }
      public string Password { get; set; }
      public string Email { get; set; }
    }
  }
}
