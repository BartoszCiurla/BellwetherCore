using Autofac;
using Bellwether.Infrastructure.Ef;
using Microsoft.EntityFrameworkCore;

namespace Bellwether.Infrastructure
{
  public class BellwetherInfrastructureModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(ThisAssembly)
             .AsImplementedInterfaces()
             .PreserveExistingDefaults();
      RegisterDatabaseAndRepositories(builder);
    }

    private static void RegisterDatabaseAndRepositories(ContainerBuilder builder)
    {
      builder.Register(ctx =>
             {
               var options = new DbContextOptionsBuilder<BellwetherContext>()
                .UseInMemoryDatabase(databaseName: "JustTest")
                .Options;

               return new BellwetherContext(options);
             })
             .As<DbContext>()
             .InstancePerDependency();
    }
  }
}
