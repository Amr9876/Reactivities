using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Application.Core;

namespace API.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next,
                               ILogger<ExceptionMiddleware> logger,
                               IHostEnvironment env)
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
 
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = _env.IsDevelopment()
                ? new AppException(context.Response.StatusCode, e.Message, e.StackTrace?.ToString())
                : new AppException(context.Response.StatusCode, "Server Error");
            
            var options = new JsonSerializerOptions 
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            string json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);

        }
    }
    
}