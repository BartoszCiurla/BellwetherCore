using System;
using Core.Domain.Ddd;

namespace Core.Application.Actors
{
    public interface IActorBootstraper
    {
        Func<IUnitOfWork> UowFactory { get; }
    }
}
