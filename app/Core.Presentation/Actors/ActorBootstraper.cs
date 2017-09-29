using System;
using Core.Presentation.Reader;
using Serilog;

namespace Core.Presentation.Actors
{
    public class ActorBootstraper : IActorBootstraper
    {
        public ActorBootstraper(Func<IReadOnlyUnitOfWork> uowFactory, ILogger logger)
        {
            UowFactory = uowFactory;
            Logger = logger;
        }

        public Func<IReadOnlyUnitOfWork> UowFactory { get; private set; }
        public ILogger Logger { get; private set; }
    }
}
