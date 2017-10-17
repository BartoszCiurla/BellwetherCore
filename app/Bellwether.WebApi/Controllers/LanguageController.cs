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
using Bellwether.Application.Api.Languages.Commands;

namespace Bellwether.WebApi.Controllers
{
  [Authorize(Roles="Admin")]
  [Route("api/[controller]")]
  public class LanguageController : BaseController
  {

    public LanguageController(ControllerBootstraper controllerBootstraper) : base(controllerBootstraper)
    {
    }

    [Route("Create")]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm]CreateLanguageCommand command)
    {
      return await SendCommand(DispatcherActorsNames.LanguageCommandActor, command);
    }

  }
}
