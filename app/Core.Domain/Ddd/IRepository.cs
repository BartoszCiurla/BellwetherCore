using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Domain.Ddd
{
    public interface IRepository<TAggregate> where TAggregate : AggregateRoot
    {
        void Add(TAggregate aggregate);
        void Update(TAggregate aggregate);
        Task Remove(Guid id);
        void Remove(TAggregate aggregate);
        IQueryable<TAggregate> Query();
        Task<TAggregate> FindById(Guid id);
        Task SaveChangesAsync();
    }
}
