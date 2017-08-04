using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Ddd;
using Core.Infrastructure.Repositories;

namespace Core.Infrastructure.Ddd
{
    public class Repository<TAggregate> : IRepository<TAggregate> where TAggregate : AggregateRoot
    {
        private readonly EfRepository<TAggregate> _efRepository;

        public Repository(EfRepository<TAggregate> efRepository)
        {
            _efRepository = efRepository;
        }

        public void Add(TAggregate aggregate)
        {
            _efRepository.Add(aggregate);
        }

        public void Update(TAggregate aggregate)
        {
            _efRepository.Update(aggregate);
        }

        public async Task Remove(Guid id)
        {
            await _efRepository.Remove(id);
        }

        public void Remove(TAggregate aggregate)
        {
            _efRepository.Remove(aggregate);
        }

        public IQueryable<TAggregate> Query()
        {
            return _efRepository.Query();
        }

        public async Task<TAggregate> FindById(Guid id)
        {
            return await _efRepository.FindById(id);
        }

        public async Task SaveChangesAsync()
        {
            await _efRepository.SaveChangesAsync();
        }
    }
}
