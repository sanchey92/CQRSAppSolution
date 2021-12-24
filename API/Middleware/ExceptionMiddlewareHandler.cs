using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddlewareHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddlewareHandler> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddlewareHandler(RequestDelegate next, 
            ILogger<ExceptionMiddlewareHandler> logger, IHostEnvironment environment)
            => (_next, _logger, _environment) = (next, logger, environment);

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _environment.IsDevelopment()
                    ? new AppException(context.Response.StatusCode, exception.Message,
                        exception.StackTrace?.ToString())
                    : new AppException(context.Response.StatusCode, "Server Error");

                var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
    
}