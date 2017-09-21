using Autofac;

namespace Bellwether.Application
{
  public class BellwetherApplicationModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterAssemblyTypes(ThisAssembly)
             .AsImplementedInterfaces()
             .PreserveExistingDefaults();
    }
  }
}
