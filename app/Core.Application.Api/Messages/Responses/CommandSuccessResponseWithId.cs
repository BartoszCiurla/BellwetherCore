using System;

namespace Core.Application.Api.Messages.Responses
{
    public class CommandSuccessResponseWithId : CommandSuccessResponse
    {
        public Guid Id { get; private set; }

        public CommandSuccessResponseWithId(Guid id)
        {
            Id = id;
        }
    }
}
