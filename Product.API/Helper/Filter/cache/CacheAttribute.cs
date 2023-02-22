using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Product.Application.Contracts.Infrastructure;
using System.Text;

namespace Product.API.Helper.Filter.cache
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public CacheAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cachedKey = GenerateCacheKeyFromRequest(context.HttpContext);
            var cachedResponse = await cacheService.GetCacheResponseAsync(cachedKey);
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                var contenetResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contenetResult;

                return;
            }

            var executedContext = await next(); // move to controller

            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheResponseAsync(cachedKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }
        private string GenerateCacheKeyFromRequest(HttpContext context)
        {

            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{context.Request.Path}");
            foreach (var (key, value) in context.Request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
