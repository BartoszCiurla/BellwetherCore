using Microsoft.EntityFrameworkCore;

using Bellwether.Infrastructure.Extensions;

namespace Bellwether.Infrastructure.Ef
{
  public class BellwetherContext : DbContext
  {
    public BellwetherContext(DbContextOptions<BellwetherContext> options)
      : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.AddEntityConfigurationsFromAssembly(GetType().Assembly);
    }
  }
}
