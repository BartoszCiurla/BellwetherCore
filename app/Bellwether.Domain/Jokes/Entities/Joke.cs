using System;
using Core.Domain.Ddd;

namespace Bellwether.Domain.Jokes.Entities
{
  public class Joke : AggregateRoot
  {
    protected Joke(Guid id) : base(id)
    {
    }
  }
}
