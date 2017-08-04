using Core.Domain.Ddd;
using Core.Infrastructure.Repositories;
using Core.Presentation.Reader;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Ddd
{
    public class ReadOnlyUnitOfWork : IReadOnlyUnitOfWork
    {
        private readonly DbContext _context;

        public ReadOnlyUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IReadOnlyRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            var efRepository = new EfRepository<TEntity>(_context);
            return new ReadOnlyRepository<TEntity>(efRepository);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
