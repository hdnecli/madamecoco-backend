using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MadameCoco.Shared.Middlewares;

/// <summary>
/// Middleware to handle global exceptions and return a standardized error response.
/// </summary>
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next delegate in the middleware pipeline.</param>
    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Invokes the middleware logic.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <returns>A task that represents the execution of the middleware.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = Response<string>.Fail(exception.Message, context.Response.StatusCode);
        
        var json = JsonSerializer.Serialize(response);
        return context.Response.WriteAsync(json);
    }
}
