using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace MackHog.Cache.Core
{
    public class CacheManager : ICache
    {
        public const string ContentKey = "CacheKeys-632d12-a7650e-cd16db2-f2b5e00941-ff5d37c8";
        public static MemoryCache Cache { get; private set; }
        private readonly CacheKeyManager _cacheKeyManager;

        public CacheManager()
        {
            Create();
            _cacheKeyManager = new CacheKeyManager(Cache);
        }

        public static void Create()
        {
            Cache = new MemoryCache(new MemoryCacheOptions());
        }

        public string CacheContentKey => ContentKey;
        public ICacheEntry CreateEntry(string key)
        {
            _cacheKeyManager.AddKey(key);
            return Cache.CreateEntry(key);
        }

        public void Remove(string key)
        {
            _cacheKeyManager.RemoveKey(key);
            Cache.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return Cache.TryGetValue(key, out value);
        }

        public IEnumerable<(string Key, object Value)> GetAll()
        {
            var cacheList = new List<(string Key, object Value)>();
            var currentKeys = _cacheKeyManager.GetKeys();
            var notFoundKeys = new List<string>();
            foreach (var key in currentKeys)
            {
                if (Cache.TryGetValue(key, out object value))
                    cacheList.Add((key, value));
                else
                    notFoundKeys.Add(key);
            }
            _cacheKeyManager.RemoveKeys(notFoundKeys);
            return cacheList;
        }

        public IEnumerable<string> GetKeys() => _cacheKeyManager.GetKeys();
    }
}
