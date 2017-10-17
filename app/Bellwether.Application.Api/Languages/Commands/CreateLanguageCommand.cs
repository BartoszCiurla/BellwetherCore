using Core.Application.Api.Messages;

namespace Bellwether.Application.Api.Languages.Commands
{
  public class CreateLanguageCommand : Command
  {
    public string Name { get; set; }
    public string ShortName { get; set; }
  }
}
