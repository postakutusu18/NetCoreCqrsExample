namespace Core.CrossCuttingConcerns.Caching;

public interface ICacheManager
{
    Task<T> GetAsync<T>(string cacheKey, CancellationToken cancellationToken);
    //Task<object> GetAsync(string key);
    //Task<object> GetAsync(string key, Type type);
    Task AddAsync(string cacheKey, object data, TimeSpan? slidingExpiration, CancellationToken cancellationToken);

    Task<T> GetOrAddAsync<T>(string cacheKey, object data, TimeSpan? slidingExpiration, CancellationToken cancellationToken);

    //void Add(string key, object data, int duration);
    //void Add(string key, object data, Type type);
    //void Add(string key, object data);
    //bool IsAdd(string key);
    Task RemoveAsync(string cacheKey, CancellationToken cancellationToken);
    Task RemoveGroupAsync(string cacheKey, CancellationToken cancellationToken);
}
