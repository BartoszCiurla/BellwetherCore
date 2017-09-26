using System.Linq;
using Core.Application.Api.Messages.Responses;
using FluentValidation.Results;

namespace Bellwether.WebApi.Core.ActionResults
{
    public static class ActionResultsExtensions
    {
        public static CommandActionResultWithId ToCommandResultWithId(this CommandSuccessResponseWithId response)
        {
            return new CommandActionResultWithId(response.Id);
        }

        public static ErrorActionResult ToErrorResult(this ErrorResponse response)
        {
            var item = new ErrorActionResultItem(null, response.ErrorMessage);
            return new ErrorActionResult(new[] {item});
        }

        public static ErrorActionResult ToErrorResult(this ValidationResult validationResult)
        {
            var items = validationResult.Errors.Select(x => new ErrorActionResultItem(x.PropertyName, x.ErrorMessage));
            return new ErrorActionResult(items);
        }
    }
}
