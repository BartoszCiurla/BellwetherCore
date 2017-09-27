using System;
using Core.Domain.Ddd;
using Serilog;

namespace Core.Application.Actors
{
  public interface IActorBootstraper
  {
    Func<IUnitOfWork> UowFactory { get; }
    ILogger Logger { get; }
  }
}
