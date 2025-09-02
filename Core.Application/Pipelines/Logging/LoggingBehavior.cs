using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Core.Application.Pipelines.Logging;


public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ILoggableRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly ILogger _logger;

    public LoggingBehavior(ILogger logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<LogParameter> parameters = new List<LogParameter>(1)
        {
            new LogParameter
            {
                Type = request.GetType().Name,
                Value = request
            }
        };
        LogDetail value = new LogDetail
        {
            MethodName = next.Method.Name,
            Parameters = parameters,
            User = (_httpContextAccessor.HttpContext.User.Identity?.Name ?? "?")
        };
        _logger.Information(JsonConvert.SerializeObject(value));
        return await next();
    }
}