using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace MackHog.Cache.Core
{
    public class Cache : ICache
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheKeyHandler _cacheKeyHandler;
        public Cache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheKeyHandler = new CacheKeyHandler(_memoryCache);
        }
        public ICacheEntry CreateEntry(string key)
        {
            _cacheKeyHandler.AddKey(key);
            return _memoryCache.CreateEntry(key);
        }

        public void Remove(string key)
        {
            _cacheKeyHandler.RemoveKey(key);
            _memoryCache.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public IEnumerable<(string Key, object Value)> GetAll()
        {
            var cacheList = new List<(string Key, object Value)>();
            var currentKeys = _cacheKeyHandler.GetKeys();
            var notFoundKeys = new List<string>();
            foreach (var key in currentKeys)
            {
                if (_memoryCache.TryGetValue(key, out object value))
                    cacheList.Add((key, value));
                else
                    notFoundKeys.Add(key);
            }
            _cacheKeyHandler.RemoveKeys(notFoundKeys);
            return cacheList;
        }

        public IEnumerable<string> GetKeys() => _cacheKeyHandler.GetKeys();
    }
}
