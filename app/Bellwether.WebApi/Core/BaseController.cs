using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentValidation.Results;
using FluentValidation;
using Akka.Actor;
using Autofac;

using Bellwether.WebApi.Core.ActionResults;
using Core.Application.Api.Messages.Responses;
using Core.Application.Api.Messages;
using NotFoundResult = Core.Application.Api.Messages.Responses.NotFoundResult;

namespace Bellwether.WebApi.Core
{
  public abstract class BaseController : Controller
  {
    protected ControllerBootstraper ControllerBootstraper { get; }

    protected BaseController(ControllerBootstraper controllerBootstraper)
    {
      ControllerBootstraper = controllerBootstraper;
    }

    protected async Task<IActionResult> SendCommand<TCommand>(string dispatcherActorName, TCommand command) where TCommand : Command
    {
      var validationResult = Validate(command);
      if (!validationResult.IsValid)
        return BadRequest(validationResult.ToErrorResult());

      var actor = await ControllerBootstraper.ActorProvider.GetOne(dispatcherActorName);
      var response = await actor.Ask(command);

      if (response == null)
        return NotFound();
      if (response is ErrorResponse)
        return BadRequest(((ErrorResponse)response).ToErrorResult());
      if (response is CommandSuccessResponseWithId)
        return Ok(((CommandSuccessResponseWithId)response).ToCommandResultWithId());
      if (response is CommandSuccessResponse)
        return Ok(new CommandActionResult());

      return BadRequest();
    }

    protected async Task<IActionResult> SendQuery<TQuery>(string dispatcherActorName, TQuery query) where TQuery : Query
    {
      var validationResult = Validate(query);
      if (!validationResult.IsValid)
        return BadRequest(validationResult.ToErrorResult());

      var actor = await ControllerBootstraper.ActorProvider.GetOne(dispatcherActorName);
      var response = await actor.Ask(query);

      if (response is NotFoundResult)
        return NotFound();
      if (response is ErrorResponse)
        return BadRequest(((ErrorResponse)response).ToErrorResult());
      if (response is QueryResult)
        return Ok(response);

      return BadRequest();
    }

    protected ValidationResult Validate<TMessage>(TMessage message)
    {
      IValidator<TMessage> validator;
      if (ControllerBootstraper.Scope.TryResolve(out validator))
        return validator.Validate(message);

      return new ValidationResult();
    }
  }
}
