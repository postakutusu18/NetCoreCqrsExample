using Core.CrossCuttingConcerns.Caching;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Pipelines.Caching;

public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICacheRemoverRequest
{
    private readonly ICacheManager _cacheManager;
    private readonly ILogger<CacheRemovingBehavior<TRequest, TResponse>> _logger;

    public CacheRemovingBehavior(ILogger<CacheRemovingBehavior<TRequest, TResponse>> logger, ICacheManager cacheManager)
    {
        _logger = logger;
        _cacheManager = cacheManager;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache)
            return await next();

        TResponse response = await next();

            await _cacheManager.RemoveGroupAsync(request.CacheGroupKey,cancellationToken);
            await _cacheManager.RemoveAsync(request.CacheKey,cancellationToken);
        return response;
    }
}
