using System;
using Core.Domain.Ddd;

namespace Bellwether.Domain.Languages.Entities
{
  public class Language : AggregateRoot
  {

    public string Name { get; private set; }
    public string ShortName { get; private set; }
    public bool IsPublic { get; private set; }
    protected Language() : base(Guid.Empty)
    {

    }

    protected Language(Guid id,
                    string name,
                    string shortName) : base(id)
    {
      Name = name;
      ShortName = shortName;
    }

    public void ChangeAvailability(bool isPublic) => IsPublic = isPublic;

    #region Factory methods
    public static Language Create(Guid id, string name, string shortName)
    {
      return new Language(id, name, shortName);
    }

    #endregion
  }
}
