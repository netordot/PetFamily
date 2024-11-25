using System.Net;
using PetFamily.Core.Models;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {

        try
        {
            await _next(httpContext); // вызов всего приложения 

        }

        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            
            var error = Error.Failure("Server.Iternal", ex.Message, null);
            var envelope = Envelope.Error(error);
            
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