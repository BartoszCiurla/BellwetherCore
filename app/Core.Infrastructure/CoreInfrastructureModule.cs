using Autofac;
using Core.Infrastructure.Ddd;
using Core.Infrastructure.Repositories;

namespace Core.Infrastructure
{
    public class CoreInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>))
                   .AsSelf();

            builder.RegisterType<UnitOfWork>()
                   .AsImplementedInterfaces();

            builder.RegisterType<ReadOnlyUnitOfWork>()
                   .AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(ReadOnlyRepository<>))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(ThisAssembly)
                   .AsImplementedInterfaces()
                   .PreserveExistingDefaults();
        }
    }
}
