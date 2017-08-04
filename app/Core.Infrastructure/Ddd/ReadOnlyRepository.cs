using System.Linq;
using Core.Infrastructure.Repositories;
using Core.Presentation.Reader;

namespace Core.Infrastructure.Ddd
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        private readonly EfRepository<TEntity> _efRepository;

        public ReadOnlyRepository(EfRepository<TEntity> efRepository)
        {
            _efRepository = efRepository;
        }

        public IQueryable<TEntity> Query()
        {
            return _efRepository.ReadOnlyQuery();
        }
    }
}
