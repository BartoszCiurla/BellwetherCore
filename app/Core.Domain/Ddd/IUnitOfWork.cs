using System;

namespace Core.Domain.Ddd
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TAggregate> GetRepository<TAggregate>()
            where TAggregate : AggregateRoot;
    }
}
