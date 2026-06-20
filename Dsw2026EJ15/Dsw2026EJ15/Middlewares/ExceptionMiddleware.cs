using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Dsw2026EJ15.Domain.Exceptions; 

namespace Dsw2026Ej15.Api.Middlewares
{
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
            catch (ValidationException ex)
            {
               
                await HandleExceptionAsync(context, ex);
            }
            catch(NotFoundException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = "Ocurrió un error inesperado";

            if (ex is ValidationException ve)
            {
                status = HttpStatusCode.BadRequest;
                message = ve.Message;
            }
            if (ex is NotFoundException)
            {
                status = HttpStatusCode.NotFound;
                message = ex.Message;
            }


            var result = JsonSerializer.Serialize(new { error = message });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            await context.Response.WriteAsync(result);
        }
    }
}