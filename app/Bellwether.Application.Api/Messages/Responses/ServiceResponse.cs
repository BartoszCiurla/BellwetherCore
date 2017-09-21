using System.Collections.Generic;

namespace Bellwether.Application.Api.Messages.Responses
{
  public class ServiceResponse
  {
    public bool Success { get; protected set; }

    public List<ServiceResponseError> Errors { get; protected set; }

    public ServiceResponse()
    {
      Success = true;
      Errors = new List<ServiceResponseError>();
    }

    public ServiceResponse WithErrors(IEnumerable<ServiceResponseError> errors)
    {
      Success = false;
      Errors.AddRange(errors);
      return this;
    }

    public ServiceResponse WithError(string name, string message)
    {
      Success = false;
      Errors.Add(new ServiceResponseError(name, message));
      return this;
    }

    public ServiceResponse WithError(string message)
    {
      return WithError(null, message);
    }
  }

  public class ServiceResponse<TData> : ServiceResponse
  {
    public TData Data { get; protected set; }

    public ServiceResponse(TData data)
    {
      Data = data;
    }

    public ServiceResponse()
    {
    }

    public ServiceResponse<TData> WithData(TData data)
    {
      Data = data;
      return this;
    }

    public new ServiceResponse<TData> WithErrors(IEnumerable<ServiceResponseError> errors)
    {
      base.WithErrors(errors);
      return this;
    }

    public new ServiceResponse<TData> WithError(string name, string message)
    {
      base.WithError(name, message);
      return this;
    }

    public new ServiceResponse<TData> WithError(string message)
    {
      return WithError(null, message);
    }
  }
}
