using System;
using System.Threading.Tasks;

using Bellwether.Application.Api.Messages.Responses;

namespace Bellwether.WebApi.Core
{
  public interface IActionExecutor
  {
    Task<ServiceResponse> ExecuteAsync(Func<Task> serviceExecution);
  }
}
