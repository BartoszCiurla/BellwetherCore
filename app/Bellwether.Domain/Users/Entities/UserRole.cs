using System;
using Core.Domain.Ddd;

namespace Bellwether.Domain.Users.Entities
{
  public class UserRole : Entity
  {
    protected UserRole(Guid id) : base(id)
    {
    }
    public Guid UserId { get; set; }
    public BellwetherUser User { get; private set; }
    public Guid RoleId { get; set; }
    public Role Role { get; private set; }
  }
}
