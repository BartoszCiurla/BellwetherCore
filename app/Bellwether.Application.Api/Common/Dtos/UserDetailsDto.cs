namespace Bellwether.Application.Api.Common.Dtos
{
  public class UserDetailsDto
  {
    public string Username { get; private set; }
    public string Email { get; private set; }

    public UserDetailsDto(string username, string email)
    {
        Username = username;
        Email = email;
    }
  }
}
