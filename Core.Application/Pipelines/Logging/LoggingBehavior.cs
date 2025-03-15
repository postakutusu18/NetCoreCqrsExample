using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Core.Application.Pipelines.Logging;


public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var parameters = new List<LogParameter>
        {
            new LogParameter
            {
                Type = request.GetType().Name,
                Value = JsonConvert.SerializeObject(request) // Parametre değerlerini JSON formatında logla
            }
        };

        var user = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "Anonymous";

        var logDetail = new LogDetail
        {
            MethodName = typeof(TRequest).Name,
            Parameters = parameters,
            User = user
        };

        _logger.LogInformation(JsonConvert.SerializeObject(logDetail)); // Log mesajını düzgün şekilde yazdır

        return await next();
    }
}
