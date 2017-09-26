using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Bellwether.Application.Api.Users;
using Bellwether.Application.Api.Common;
using Bellwether.Application.Users;
using Bellwether.Domain.Users.Entities;
using Bellwether.WebApi.Core;
using Core.Domain.Ddd;

namespace Bellwether.WebApi.Controllers
{
  [Route("api/[controller]")]
  public class AccountController : BaseController
  {

    public AccountController(ControllerBootstraper controllerBootstraper) : base(controllerBootstraper)
    {
    }

    [Route("RegisterUser")]
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromForm]RegisterUserCommand command)
    {
      return await SendCommand(DispatcherActorsNames.UserCommandActor, command);
    }
  }
}
