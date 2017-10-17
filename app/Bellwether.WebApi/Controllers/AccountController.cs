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
using Microsoft.AspNetCore.Authorization;
using Bellwether.Application.Api.Users.Queries;

namespace Bellwether.WebApi.Controllers
{
  [Authorize(Roles = "Admin")]
  [Route("api/[controller]")]
  public class AccountController : BaseController
  {

    public AccountController(ControllerBootstraper controllerBootstraper) : base(controllerBootstraper)
    {
    }

    [Route("RegisterUser")]
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromForm]RegisterUserCommand command)
    {
      return await SendCommand(DispatcherActorsNames.UserCommandActor, command);
    }

    [Route("GetUsers")]
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
      return await SendQuery(DispatcherActorsNames.UserQueryActor, new GetUsersQuery());
    }
  }
}
