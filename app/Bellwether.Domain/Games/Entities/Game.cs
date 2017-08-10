using System;
using Core.Domain.Ddd;

namespace Bellwether.Domain.Games.Entities
{
  public class Game : AggregateRoot
  {
    protected Game(Guid id) : base(id)
    {
    }
  }
}
