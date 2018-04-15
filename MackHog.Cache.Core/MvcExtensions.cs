using Microsoft.Extensions.DependencyInjection;

namespace MackHog.Cache.Core
{
    public static class MvcExtensions
    {
        public static void AddCache(this IServiceCollection services)
        {
            services.AddSingleton<ICache>(new CacheManager());
        }
    }
}
