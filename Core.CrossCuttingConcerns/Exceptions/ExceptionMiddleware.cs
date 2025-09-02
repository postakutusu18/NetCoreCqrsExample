using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Core.CrossCuttingConcerns.Logging;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using Core.CrossCuttingConcerns.Logging.Serilog.Abstraction;

namespace Core.CrossCuttingConcerns.Exceptions;
public class ExceptionMiddleware
{
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly HttpExceptionHandler _httpExceptionHandler;
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, ILogger logger)
    {
        _next = next;
        _contextAccessor = contextAccessor;
        _logger = logger;
        _httpExceptionHandler = new HttpExceptionHandler();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await LogException(context, exception);
            await HandleExceptionAsync(context.Response, exception);
        }
    }

    private Task HandleExceptionAsync(HttpResponse response, Exception exception)
    {
        response.ContentType = "application/json";
        _httpExceptionHandler.Response = response;
        return _httpExceptionHandler.HandleExceptionAsync(exception);
    }

    private Task LogException(HttpContext context, Exception exception)
    {
        List<LogParameter> logParameters =
            new()
            {
                new LogParameter { Type = context.GetType().Name, Value = exception.ToString(),Name =context.Request.Method }
            };

        LogDetail logDetail =
            new()
            {
                MethodName = context.Request.Path,
                Parameters = logParameters,
                User = _contextAccessor.HttpContext?.User.Identity?.Name ?? "?"
            };

        _logger.Error(JsonSerializer.Serialize(logDetail));
        return Task.CompletedTask;
    }
}
