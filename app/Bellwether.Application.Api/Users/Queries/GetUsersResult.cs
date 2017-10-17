using System.Collections.Generic;
using Bellwether.Application.Api.Common.Dtos;
using Core.Application.Api.Messages;

namespace Bellwether.Application.Api.Users.Queries
{
  public class GetUsersResult: QueryResult
  {
    public List<UserDetailsDto> Users { get; private set; }
    public GetUsersResult(List<UserDetailsDto> users)
    {
        Users = users;
    }
  }
}
