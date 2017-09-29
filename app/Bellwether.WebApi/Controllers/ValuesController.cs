using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Bellwether.WebApi.Core;
using System.Threading.Tasks;
using Bellwether.Application.Api.Common;
using Bellwether.Application.Api;

namespace Bellwether.WebApi.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class ValuesController : BaseController
  {
    public ValuesController(ControllerBootstraper controllerBootstraper) : base(controllerBootstraper)
    {
    }

    // GET api/values
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      return await SendQuery(DispatcherActorsNames.ValuesQueryActor, new GetValuesQuery());
    }

    // GET api/values/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

    // POST api/values
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT api/values/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/values/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
