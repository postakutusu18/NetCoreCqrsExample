using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Core.CrossCuttingConcerns.Caching;

public class MemoryCacheManager : ICacheManager
{
    private readonly IMemoryCache _cache;
    private readonly CacheSettings _cacheSettings;

    public MemoryCacheManager(IConfiguration configuration, IMemoryCache memoryCache)
    // : this(ServiceTool.ServiceProvider.GetService<IMemoryCache>())
    {
        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>() ?? throw new InvalidOperationException();
        _cache = memoryCache;
    }

    public MemoryCacheManager(IMemoryCache cache)
    {
        _cache = cache;
    }
    public async Task<T> GetAsync<T>(string cacheKey, CancellationToken cancellationToken)
    {
        return _cache.Get<T>(cacheKey);

    }
    public async Task AddAsync(string cacheKey, object data, TimeSpan? slidingExpiration, CancellationToken cancellationToken)
    {
        _cache.Set(cacheKey, data, slidingExpiration.Value);
    }

    public async Task<T> GetOrAddAsync<T>(string cacheKey, object data, TimeSpan? slidingExpiration, CancellationToken cancellationToken)
    {
        var cache = _cache.Get<T>(cacheKey);
        if (cache != null)
            return cache;
        _cache.Set(cacheKey, data, slidingExpiration.Value);
        cache = _cache.Get<T>(cacheKey);
        return cache;
    }
    public async Task RemoveAsync(string key, CancellationToken cancellationToken)
    {
        if (key != null)
            _cache.Remove(key);
    }

    public async Task RemoveGroupAsync(string cacheKey, CancellationToken cancellationToken)
    {
        if (cacheKey != null)
        {
            var cachedGroup = _cache.Get<HashSet<string>>(cacheKey);
            if (cachedGroup != null)
            {
                foreach (var key in cachedGroup)
                {
                    _cache.Remove(key);
                }
                _cache.Remove(cacheKey);
            }
        }
    }

    public void ClearAllCaches()
    {
        var fieldInfo = typeof(MemoryCache).GetField("_coherentState", BindingFlags.Instance | BindingFlags.NonPublic);
        var propertyInfo = fieldInfo.FieldType.GetProperty("EntriesCollection", BindingFlags.Instance | BindingFlags.NonPublic);
        var value = fieldInfo.GetValue(_cache);
        var dict = propertyInfo.GetValue(value) as dynamic;

        List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();

        foreach (var cacheItem in dict)
        {
            ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
            cacheCollectionValues.Add(cacheItemValue);
        }

        foreach (var cacheEntry in cacheCollectionValues)
        {
            _cache.Remove(cacheEntry.Key);
        }
    }

    public object Get(string key)
    {
        _cache.TryGetValue(key, out var data);
        return data;
    }


}
