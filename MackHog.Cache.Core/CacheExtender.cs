using Microsoft.Extensions.DependencyInjection;

namespace MackHog.Cache.Core
{
    public static class CacheExtender
    {
        public static void AddCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<ICache, Cache>();
        }
    }
}
