using Microsoft.Extensions.Caching.Distributed;
using Product.Application.Contracts.Infrastructure;
using System.Text.Json;

namespace Product.Infrastructure.Services
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;
        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)
            {
                return;
            }
            //DefaultContractResolver contractResolver = new DefaultContractResolver
            //{
            //    NamingStrategy = new CamelCaseNamingStrategy()
            //};
            //var serializedResponse = JsonConvert.SerializeObject(response,new JsonSerializerSettings { 
            // ContractResolver = contractResolver,
            // Formatting = Formatting.Indented
            //});
            var serializedResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await _distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var cacheResponse = await _distributedCache.GetStringAsync(cacheKey);
            return string.IsNullOrEmpty(cacheResponse) ? null : cacheResponse;
        }

        public async Task<bool> RemoveCacheResponseAsync(string cacheKey)
        {
            try
            {
                await _distributedCache.RemoveAsync(cacheKey);
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
