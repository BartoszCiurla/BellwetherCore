using System;
using Core.Domain.Ddd;
using Microsoft.Extensions.Logging;

namespace Bellwether.WebApi.Core
{
  public class ControllerBootstraper : IControllerBootstraper
  {
    public ControllerBootstraper(Func<IUnitOfWork> uowFactory, ILogger logger)
    {
      UowFactory = uowFactory;
      Logger = logger;
    }

    public Func<IUnitOfWork> UowFactory { get; }
    public ILogger Logger { get; }
  }
}
