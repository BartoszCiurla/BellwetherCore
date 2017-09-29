using System;
using Core.Presentation.Reader;
using Serilog;

namespace Core.Presentation.Actors
{
    public interface IActorBootstraper
    {
        Func<IReadOnlyUnitOfWork> UowFactory { get; }
        ILogger Logger { get; }
    }
}
