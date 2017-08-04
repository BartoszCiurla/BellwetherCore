using System.Linq;

namespace Core.Presentation.Reader
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Query();
    }
}
