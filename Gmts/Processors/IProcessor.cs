using Gmts.Models;

namespace Gmts.Processors
{
    public interface IProcessor
    {
        ProcessedCacheData Process(CacheData cacheData);
    }
}