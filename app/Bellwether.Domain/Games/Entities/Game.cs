using System;
using Core.Domain.Ddd;

namespace Bellwether.Domain.Games.Entities
{
    public class Game : AggregateRoot
    {
        protected Game() : base(Guid.Empty)
        {
        }
        protected Game(Guid id):base(id)
        {

        }

        public static Game Create(Guid id)
        {
            return new Game(id);
        }

    }
}
