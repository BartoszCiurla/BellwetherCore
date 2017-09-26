namespace Core.Application.Api.Messages.Responses
{
  public class ErrorResponse
  {
    public string ErrorMessage { get; private set; }

    public ErrorResponse(string errorMessage)
    {
      ErrorMessage = errorMessage;
    }
  }
}
