using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Core.Application.Api.Messages;
using Core.Application.Api.Messages.Responses;
using Core.Domain.Ddd;
using Core.Presentation.Reader;
using Serilog;

namespace Core.Presentation.Actors
{
  public class BaseActor : ReceiveActor
  {
    private readonly IActorBootstraper _actorBootstraper;

    protected ILogger Logger => _actorBootstraper.Logger;

    public BaseActor(IActorBootstraper actorBootstraper)
    {
      _actorBootstraper = actorBootstraper;
    }

    private async Task HandleMessage<TMessage>(TMessage query, Func<IReadOnlyUnitOfWork, Task> action) where TMessage : Query
    {
      try
      {
        using (var uow = _actorBootstraper.UowFactory())
        {
          await action(uow);
        }

        Logger.ForContext<BaseActor>().Debug("Query {Query} successfuly handled.", query);
      }
      catch (Exception exception)
      {
        Logger.ForContext<BaseActor>().Fatal(exception, "Error occured during handling query {Query}", query);
        Sender.Tell(new ErrorResponse("GENERAL ERROR"));
      }
    }

    protected async Task HandleQuery<TMessage, TResult>(TMessage message, Func<IReadOnlyUnitOfWork, TResult> action)
        where TMessage : Query
        where TResult : QueryResult
    {
      await HandleMessage(message, uow =>
      {
        var result = action(uow);

        if (result != null)
          Sender.Tell(result);
        else
          Sender.Tell(new NotFoundResult());

        return Task.CompletedTask;
      });
    }

    protected async Task HandleQuery<TMessage, TResult>(TMessage message, Func<IReadOnlyUnitOfWork, Task<TResult>> action)
        where TMessage : Query
        where TResult : QueryResult
    {
      await HandleMessage(message, async uow =>
      {
        var result = await action(uow);

        if (result != null)
          Sender.Tell(result);
        else
          Sender.Tell(new NotFoundResult());
      });
    }
  }
}
