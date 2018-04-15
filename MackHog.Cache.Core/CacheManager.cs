using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace MackHog.Cache.Core
{
    public class CacheManager : ICache
    {
        public const string ContentKey = "CacheKeys-632d12-a7650e-cd16db2-f2b5e00941-ff5d37c8";
        public static MemoryCache Cache { get; private set; }

        public CacheManager()
        {
            Create();
        }

        public static void Create()
        {
            Cache = new MemoryCache(new MemoryCacheOptions());
        }
        
        public ICacheEntry CreateEntry(string key)
        {
            AddKey(key);
            return Cache.CreateEntry(key);
        }

        public void Remove(string key)
        {
            RemoveKey(key);
            Cache.Remove(key);
        }

        public bool TryGetValue(string key, out object value)
        {
            return Cache.TryGetValue(key, out value);
        }

        public IEnumerable<(string Key, object Value)> GetAll()
        {
            var cacheList = new List<(string Key, object Value)>();
            var currentKeys = GetKeys();
            var notFoundKeys = new List<string>();
            foreach (var key in currentKeys)
            {
                if (Cache.TryGetValue(key, out object value))
                    cacheList.Add((key, value));
                else
                    notFoundKeys.Add(key);
            }
            RemoveKeys(notFoundKeys);
            return cacheList;
        }
        
        public List<string> GetKeys()
        {
            if (Cache.TryGetValue(CacheManager.ContentKey, out object objVal))
            {
                return (List<string>)objVal;
            }
            return new List<string>();
        }

        private void UpdateKeys(List<string> keys)
        {
            using (var item = Cache.CreateEntry(CacheManager.ContentKey))
            {
                item.Value = keys;
            }
        }

        private void RemoveKey(string key)
        {
            var currentKeys = GetKeys();
            currentKeys.Remove(key);
            UpdateKeys(currentKeys);
        }

        private void RemoveKeys(List<string> keys)
        {
            if (keys.Any())
            {
                var currentKeys = GetKeys();
                foreach (var key in keys)
                {
                    if (currentKeys.Contains(key))
                        currentKeys.Remove(key);
                }
                UpdateKeys(currentKeys);
            }
        }

        private void AddKey(string key)
        {
            var keys = GetKeys();
            if (!keys.Contains(key))
            {
                keys.Add(key);
                UpdateKeys(keys);
            }
        }
    }
}
