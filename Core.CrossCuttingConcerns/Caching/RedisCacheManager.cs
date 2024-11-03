using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Caching;

public class RedisCacheManager : ICacheManager
{
    private readonly IDistributedCache _cache;
    private readonly CacheSettings _cacheSettings;

    public RedisCacheManager(IDistributedCache cache, IConfiguration configuration)
    {
        _cache = cache;
        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>() ?? throw new InvalidOperationException();
    }

    public async Task<T> GetAsync<T>(string cacheKey, CancellationToken cancellationToken)
    {
        var value = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(value))
        {
            return JsonSerializer.Deserialize<T>(value);
        }
        return default;
    }

    public async Task AddAsync(string cacheKey, object data, TimeSpan? slidingExpiration, CancellationToken cancellationToken)
    {
        TimeSpan expiration = slidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration);
        DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = expiration };
        byte[] serializeData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
        await _cache.SetAsync(cacheKey, serializeData, cacheOptions, cancellationToken);
    }


    public async Task<T> GetOrAddAsync<T>(string cacheKey, object data, TimeSpan? slidingExpiration, CancellationToken cancellationToken)
    {
        var value = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(value))
            return JsonSerializer.Deserialize<T>(value);
     
        TimeSpan expiration = slidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration);
        DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = expiration };
        byte[] serializeData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));
        await _cache.SetAsync(cacheKey, serializeData, cacheOptions, cancellationToken);
        
        value = await _cache.GetStringAsync(cacheKey, cancellationToken);
        return JsonSerializer.Deserialize<T>(value);
    }


    public async Task RemoveAsync(string cacheKey, CancellationToken cancellationToken)
    {
        if (cacheKey != null)
            await _cache.RemoveAsync(cacheKey, cancellationToken);
    }
    public async Task RemoveGroupAsync(string cacheKey, CancellationToken cancellationToken)
    {
        if (cacheKey != null)
        {
            byte[]? cachedGroup = await _cache.GetAsync(cacheKey, cancellationToken);
            if (cachedGroup != null)
            {
                var keysInGroup = JsonSerializer.Deserialize<HashSet<string>>(Encoding.Default.GetString(cachedGroup))!;
                foreach (string key in keysInGroup)
                {
                    await _cache.RemoveAsync(key, cancellationToken);
                }
                await _cache.RemoveAsync(cacheKey, cancellationToken);
            }
        }
    }

}
