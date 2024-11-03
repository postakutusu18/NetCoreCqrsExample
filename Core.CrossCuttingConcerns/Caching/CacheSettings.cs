namespace Core.CrossCuttingConcerns.Caching;

public class CacheSettings
{
    public bool UseDistributedCache { get; set; }
    public int SlidingExpiration { get; set; }
}
