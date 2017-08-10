using System;
using Core.Domain.Ddd;

namespace Bellwether.Domain.Jokes.Entities
{
  public class JokeDetail : AggregateRoot
  {
    protected JokeDetail(Guid id) : base(id)
    {
    }
  }
}
