using System;

namespace SpiceRack.Core.Application.Wrappers;

public class Response<T>
{
  public bool Succeded { get; set; }
  public string? Message { get; set; }
  public List<string>? Errors { get; set; }
  public T Data { get; set; }

  public Response(){}

  public Response(T data, string? message = null)
  {
    Succeded = true;
    Message = message ?? "";
    Data = data;
  }

  public Response(string? message)
  {
    Succeded = false;
    Message = message;
  }
}
