using System.Net;
using PetFamily.API.Response;

namespace PetFamily.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {

        try
        {
            await _next(httpContext); // вызов всего приложения 

        }

        catch(Exception ex)
        {
            var responseError = new ResponseError("Server.Iternal", ex.Message, null);
            var envelope = Envelope.Error([responseError]);
            
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
            
            await httpContext.Response.WriteAsJsonAsync(envelope);
        }
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}