using System;

namespace Core.Domain.Ddd
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity(Guid id)
        {
            Id = id;
        }
    }
}
