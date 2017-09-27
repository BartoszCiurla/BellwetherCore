using System;
using Core.Domain.Ddd;
using Serilog;

namespace Core.Application.Actors
{
  public class ActorBootstraper : IActorBootstraper
  {
    public ActorBootstraper(Func<IUnitOfWork> uowFactory, ILogger logger)
    {
      UowFactory = uowFactory;
      Logger = logger;
    }
    public ILogger Logger { get; private set; }
    public Func<IUnitOfWork> UowFactory { get; private set; }
  }
}
