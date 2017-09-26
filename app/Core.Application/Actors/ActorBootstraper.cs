using System;
using Core.Domain.Ddd;

namespace Core.Application.Actors
{
   public class ActorBootstraper : IActorBootstraper
    {
        public ActorBootstraper(Func<IUnitOfWork> uowFactory)
        {
            UowFactory = uowFactory;
        }

        public Func<IUnitOfWork> UowFactory { get; private set; }
    }
}
