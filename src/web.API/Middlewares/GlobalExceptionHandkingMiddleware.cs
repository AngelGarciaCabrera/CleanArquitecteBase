using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Web.API.Middlewares
{
    public class GlobalExceptionHandkingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandkingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionHandkingMiddleware(ILogger<GlobalExceptionHandkingMiddleware> logger, IWebHostEnvironment env)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (KeyNotFoundException ex) // Capturar errores de "No encontrado"
            {
                _logger.LogWarning(ex, ex.Message);
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, "Recurso no encontrado", ex.Message);
            }
            catch (DbUpdateException ex) // Capturar errores de base de datos
            {
                _logger.LogError(ex, ex.InnerException?.Message ?? ex.Message);
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, "Error al guardar datos en la base", ex.InnerException?.Message ?? ex.Message);
            }
            catch (Exception ex) // Manejo general para otros errores
            {
                _logger.LogError(ex, ex.Message);
                string detail = _env.IsDevelopment() ? ex.Message : "Ocurrió un error interno en el servidor";
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "Error en el servidor", detail);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string title, string detail)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var problem = new ProblemDetails
            {
                Status = (int)statusCode,
                Type = statusCode.ToString(),
                Title = title,
                Detail = detail
            };

            var json = JsonSerializer.Serialize(problem);
            await context.Response.WriteAsync(json);
        }
    }
}
