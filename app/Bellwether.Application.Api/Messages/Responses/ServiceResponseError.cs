namespace Bellwether.Application.Api.Messages.Responses
{
  public class ServiceResponseError
  {
    public string Name { get; protected set; }
    public string Message { get; protected set; }

    public ServiceResponseError(string name, string message)
    {
      Name = name;
      Message = message;
    }
  }
}
