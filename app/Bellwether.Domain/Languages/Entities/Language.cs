using System;
using Core.Domain.Ddd;

namespace Bellwether.Domain.Languages.Entities
{
  public class Language : AggregateRoot
  {
    protected Language() : base(Guid.Empty)
    {

    }

    public Language(Guid id,
                    string name,
                    string shortName,
                    bool isPublic) : base(id)
    {
      Name = name;
      ShortName = shortName;
      IsPublic = isPublic;
    }

    public string Name { get; private set; }
    public string ShortName { get; private set; }
    public bool IsPublic { get; private set; }
  }
}
