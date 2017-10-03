using Autofac;
using Microsoft.EntityFrameworkCore;
using Serilog.AspNetCore;

using Bellwether.Infrastructure.Ef;
using Serilog;
using System.IO;
using Microsoft.Extensions.Configuration;

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

      RegisterLogger(builder);
    }

    private static void RegisterLogger(ContainerBuilder builder)
    {
       builder.Register(ctx => new LoggerConfiguration()
                                 .MinimumLevel.Debug()
                                 .WriteTo.RollingFile(Path.Combine("Logs","Bellwether-{Date}.txt"))
                                 .WriteTo.Trace()
                                 .WriteTo.Console()
                                 .CreateLogger())
                   .As<ILogger>()
                   .SingleInstance();
    }

    private static void RegisterDatabaseAndRepositories(ContainerBuilder builder)
    {
      builder.Register(ctx =>
             {
               var configuration = ctx.Resolve<IConfiguration>();
               var connectionString = configuration.GetConnectionString("BellwetherDatabase");

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
