using System;
using Core.Domain.Ddd;

namespace Bellwether.Domain.Games.Entities
{
  public class GameDetail : AggregateRoot
  {
    protected GameDetail(Guid id) : base(id)
    {
    }
  }
}
