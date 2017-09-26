using System;

namespace Bellwether.WebApi.Core.ActionResults
{
    public class CommandActionResultWithId
    {
        public Guid Id { get; }

        public CommandActionResultWithId(Guid id)
        {
            Id = id;
        }
    }
}
