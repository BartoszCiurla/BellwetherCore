using System.Collections.Generic;
using Core.Application.Api.Messages;

namespace Bellwether.Application.Api
{
  public class GetValuesResult : QueryResult
  {
    public List<string> Values { get; set; }
  }
}
