using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System;
using Core.Domain.Ddd;

namespace Core.Infrastructure.Repositories
{
  public class EfRepository<TAggregate> where TAggregate : class
  {
    private DbContext _context;
    private DbSet<TAggregate> _set;

    public EfRepository(DbContext context)
    {
      _context = context;
      _set = _context.Set<TAggregate>();
    }

    public void Add(TAggregate aggregate)
    {
      _set.Add(aggregate);
    }

    public void Update(TAggregate aggregate)
    {
      _set.Update(aggregate);
    }

    public async Task Remove(Guid id)
    {
      var entity = await _set.FindAsync(id);
      _set.Remove(entity);
    }

    public void Remove(TAggregate aggregate)
    {
      _set.Remove(aggregate);
    }

    public IQueryable<TAggregate> Query()
    {
      return _set.AsQueryable();
    }

    public IQueryable<TAggregate> ReadOnlyQuery()
    {
      return _set.AsNoTracking();
    }

    public async Task<TAggregate> FindById(Guid id)
    {
      return await _set.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }
  }
}
