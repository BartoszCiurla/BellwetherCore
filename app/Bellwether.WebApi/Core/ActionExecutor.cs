using System;
using System.Threading.Tasks;

using Bellwether.Application.Api.Messages.Responses;

namespace Bellwether.WebApi.Core
{
  public class ActionExecutor : IActionExecutor
  {
    public async Task<ServiceResponse> ExecuteAsync(Func<Task> serviceExecution)
    {
      var response = new ServiceResponse();
      try
      {
        await serviceExecution();
        return response;
      }
      catch(Exception exception)
      {
        return response.WithError(exception.Message);
      }
    }
  }
}
