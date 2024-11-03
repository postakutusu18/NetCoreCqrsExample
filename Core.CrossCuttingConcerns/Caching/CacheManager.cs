using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Core.CrossCuttingConcerns.Caching;

//public class CacheManager : ICacheManager
//{
//    private readonly IDistributedCache _distributedCache;
//    private readonly IMemoryCache _memoryCache;
//    private readonly CacheSettings _cacheSettings;
//    private readonly bool _useDistributedCache;

//    public CacheManager(IDistributedCache distributedCache, IMemoryCache memoryCache, IConfiguration configuration)
//    {
//        _distributedCache = distributedCache;
//        _memoryCache = memoryCache;
//        _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>() ?? throw new InvalidOperationException();

//        // Burada hangi cache türünü kullanacağınızı belirleyebilirsiniz
//        _useDistributedCache = _cacheSettings.UseDistributedCache;
//    }

//    public async Task<T?> GetAsync<T>(string cacheKey, CancellationToken cancellationToken = default)
//    {
//        if (_useDistributedCache)
//        {
//            var cachedData = await _distributedCache.GetAsync(cacheKey, cancellationToken);
//            if (cachedData != null)
//                return JsonSerializer.Deserialize<T>(Encoding.Default.GetString(cachedData));
//        }
//        else
//        {
//            var resultValue = _memoryCache.Get<T>(cacheKey); // Veriyi doğrudan alıyoruz
//            if (resultValue != null)
//            {
//                return resultValue; // Nesneyi direkt döndürüyoruz, Deserialize gerekmez.
//            }
//        }
//        return default;
//    }

//    public async Task SetAsync<T>(string cacheKey, T value, TimeSpan? slidingExpiration, CancellationToken cancellationToken = default)
//    {
//        if (_useDistributedCache)
//        {
//            var options = new DistributedCacheEntryOptions
//            {
//                SlidingExpiration = slidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration)
//            };
//            var serializedData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));
//            await _distributedCache.SetAsync(cacheKey, serializedData, options, cancellationToken);
//        }
//        else
//        {
//            var options = new MemoryCacheEntryOptions
//            {
//                SlidingExpiration = slidingExpiration ?? TimeSpan.FromDays(_cacheSettings.SlidingExpiration)
//            };
//            _memoryCache.Set(cacheKey, value, options);
//        }
//    }

//    public async Task RemoveAsync(string cacheKey, CancellationToken cancellationToken = default)
//    {
//        if (_useDistributedCache)
//        {
//            await _distributedCache.RemoveAsync(cacheKey, cancellationToken);
//        }
//        else
//        {
//            _memoryCache.Remove(cacheKey);
//        }
//    }
//}
