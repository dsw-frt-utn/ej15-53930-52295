using System.Net;
using System.Text.Json;
using Dsw2026EJ15.Domain;
using Dsw2026EJ15.Domain.Exceptions;

namespace Dsw2026EJ15.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;
        var message = "Ocurrió un error inesperado en el servidor.";

        if (exception is ValidationException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = exception.Message;
        }

        context.Response.StatusCode = (int)statusCode;
        var response = new { error = message };

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}