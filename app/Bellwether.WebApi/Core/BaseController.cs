using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bellwether.WebApi.Core
{
  public abstract class BaseController : Controller
  {
    private readonly IActionExecutor _actionExecutor;

    protected BaseController(IActionExecutor actionExecutor)
    {
      _actionExecutor = actionExecutor;
    }

    protected async Task<IActionResult> HandleAction<T>(Func<Task<T>> action)
    {
      var response = await _actionExecutor.ExecuteAsync(action);
      if (response == null)
        return NotFound();

      return BadRequest();
    }
    protected async Task<IActionResult> HandleAction(Func<Task> action)
    {
      var response = await _actionExecutor.ExecuteAsync(action);
      if (response == null)
        return NotFound();
      return BadRequest();
    }

  }
}
