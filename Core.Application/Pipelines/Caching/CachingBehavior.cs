using System.Text;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Core.CrossCuttingConcerns.Caching;

namespace Core.Application.Pipelines.Caching;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly ICacheManager _cacheManager;
    private readonly CacheSettings _cacheSettings;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    public CachingBehavior(
        ILogger<CachingBehavior<TRequest, TResponse>> logger, IConfiguration configuration, ICacheManager cacheManager)
    {
        //_cache = cache;
        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>() ?? throw new InvalidOperationException();
        _logger = logger;
        _cacheManager = cacheManager;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request.BypassCache)
            return await next();
        TResponse response;
        var cacheResponse = await _cacheManager.GetAsync<TResponse>(request.CacheKey,cancellationToken);
        if (cacheResponse != null)
        {
            response = cacheResponse;
            _logger.LogInformation($"Fetched from Cache -> {request.CacheKey}");
        }
        else
        {
            response = await getResponseAndAddToCache(request,next,cancellationToken);
        }
        return response;   
    }
    private async Task<TResponse> getResponseAndAddToCache(
       TRequest request,
       RequestHandlerDelegate<TResponse> next,
       CancellationToken cancellationToken
   )
    {
        TResponse response = await next();

        TimeSpan? slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration);
        await _cacheManager.AddAsync(request.CacheKey, response, slidingExpiration, cancellationToken);
        _logger.LogInformation($"Added to Cache -> {request.CacheKey}");

         if (request.CacheGroupKey != null)
        {
            HashSet<string> keysInGroup = await _cacheManager.GetAsync<HashSet<string>>(request.CacheGroupKey, cancellationToken) ?? new HashSet<string>();
            if (!keysInGroup.Contains(request.CacheKey))
            {
                keysInGroup.Add(request.CacheKey);
                await _cacheManager.AddAsync(request.CacheGroupKey, keysInGroup, slidingExpiration, cancellationToken);
                _logger.LogInformation($"Added to Cache -> {request.CacheGroupKey}");
            }
        }

        return response;
    }
}