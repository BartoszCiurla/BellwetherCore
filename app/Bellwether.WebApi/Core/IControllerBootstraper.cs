using System;
using Core.Domain.Ddd;
using Microsoft.Extensions.Logging;

namespace Bellwether.WebApi.Core
{
  public interface IControllerBootstraper
  {
    Func<IUnitOfWork> UowFactory {get;}
    ILogger Logger {get;}
  }
}
