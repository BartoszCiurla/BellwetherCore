using System;
using System.Collections.Generic;
using Core.Domain.Ddd;

namespace Bellwether.Domain.Users.Entities
{
  public class Role : AggregateRoot
  {
    protected Role(Guid id) : base(id)
    {
    }
    public string Type { get; private set; }
    public virtual ICollection<UserRole> UserRoles { get; private set; }
  }
}
