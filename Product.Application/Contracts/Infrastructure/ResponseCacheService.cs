using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Contracts.Infrastructure
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string> GetCacheResponseAsync(string cacheKey);

        Task<bool> RemoveCacheResponseAsync(string cacheKey);
    }
}
