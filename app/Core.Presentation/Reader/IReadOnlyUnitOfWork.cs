using System;

namespace Core.Presentation.Reader
{
    public interface IReadOnlyUnitOfWork : IDisposable
    {
        IReadOnlyRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;
    }
}
