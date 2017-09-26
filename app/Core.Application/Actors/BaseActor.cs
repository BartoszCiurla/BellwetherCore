using System;
using System.Threading.Tasks;
using Akka.Actor;
using Core.Application.Api.Messages.Responses;
using Core.Application.Api.Messages;
using Core.Domain.Ddd;

namespace Core.Application.Actors
{
  public class BaseActor : ReceiveActor
  {
    private readonly IActorBootstraper _actorBootstraper;

    public BaseActor(IActorBootstraper actorBootstraper)
    {
      _actorBootstraper = actorBootstraper;
    }

    private async Task HandleMessage<TMessage>(TMessage command, Func<IUnitOfWork, Task> action) where TMessage : class
    {
      try
      {
        using (var uow = _actorBootstraper.UowFactory())
        {
          await action(uow);
        }
      }
      catch (Exception exception)
      {
        Sender.Tell(new ErrorResponse("GENERAL ERROR"));
      }
    }

    protected async Task HandleCommand<TMessage>(TMessage message, Action<IUnitOfWork> action) where TMessage : Command
    {
      await HandleMessage(message, uow =>
      {
        action(uow);
        Sender.Tell(new CommandSuccessResponse());
        return Task.CompletedTask;
      });
    }

    protected async Task HandleCommand<TMessage>(TMessage message, Func<IUnitOfWork, Guid> action) where TMessage : Command
    {
      await HandleMessage(message, uow =>
      {
        var id = action(uow);
        Sender.Tell(new CommandSuccessResponseWithId(id));
        return Task.CompletedTask;
      });
    }

    protected async Task HandleCommand<TMessage>(TMessage message, Func<IUnitOfWork, Task> action) where TMessage : Command
    {
      await HandleMessage(message, async uow =>
      {
        await action(uow);
        Sender.Tell(new CommandSuccessResponse());
      });
    }

    protected async Task HandleCommand<TMessage>(TMessage message, Func<IUnitOfWork, Task<Guid>> action) where TMessage : Command
    {
      await HandleMessage(message, async uow =>
      {
        var id = await action(uow);
        Sender.Tell(new CommandSuccessResponseWithId(id));
      });
    }
  }
}
