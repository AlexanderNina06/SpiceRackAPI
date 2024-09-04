using System.Net;
using System.Text.Json;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.WebApi.Middlewares;

public class ErrorHandlerMiddleware
{
  private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);

        }catch(Exception error)
        {
            var response = httpContext.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string>() { Succeded = false, Message = error?.Message};

            switch(error)
            {
                case ApiException e: 
                        switch(e.ErrorCode)
                        {
                            case (int)HttpStatusCode.BadRequest:
                                response.StatusCode = (int)HttpStatusCode.BadRequest;
                                break;
                            case (int)HttpStatusCode.NotFound:
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                break;
                            default:
                                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                break;
                        }
                    break;
                case KeyNotFoundException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);
            await response.WriteAsync(result);
        }
    }

}
