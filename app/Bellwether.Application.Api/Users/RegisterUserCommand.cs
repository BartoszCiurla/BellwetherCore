namespace Bellwether.Application.Api.Users
{
  public class RegisterUserCommand
  {
    public string Surname { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
  }
}
