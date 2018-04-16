using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MackHog.Cache.Core
{
    public class CacheManager : ICache
    {
        public CacheManager()
        {
            Cache.Create();
        }

        public void Add(string key, object value, DateTimeOffset? absoluteExpiration = null, TimeSpan? slidingExpiration = null) =>
         Cache.Add(key, value, absoluteExpiration, slidingExpiration);

        public void AddMany(IEnumerable<(string Key, object Value, DateTimeOffset? AbsoluteExpiration, TimeSpan? SlidingExpiration)> entries) =>
            Cache.AddMany(entries);

        public void Remove(string key) => Cache.Remove(key);

        public bool TryGetValue(string key, out object value) => Cache.TryGetValue(key, out value);

        public IEnumerable<(string Key, object Value)> GetAll() => Cache.GetAll();
    }

    public static class Cache
    {
        private static MemoryCache _memoryCache { get; set; }
        private const string ContentKey = "CacheKeys-632d12-a7650e-cd16db2-f2b5e00941-ff5d37c8";

        internal static void Create()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public static void Reset() => Create();

        public static void Add(
            string key,
            object value,
            DateTimeOffset? absoluteExpiration = null,
            TimeSpan? slidingExpiration = null)
        {
            using (var entry = _memoryCache.CreateEntry(key))
            {
                entry.Value = value;
                entry.AbsoluteExpiration = absoluteExpiration;
                entry.SlidingExpiration = slidingExpiration;
            }
        }

        public static void AddMany(IEnumerable<(
            string Key,
            object Value,
            DateTimeOffset? AbsoluteExpiration,
            TimeSpan? SlidingExpiration)> entries)
        {
            foreach (var val in entries)
            {
                Add(val.Key, val.Value, val.AbsoluteExpiration, val.SlidingExpiration);
            }
        }

        public static bool TryGetValue(string key, out object value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }

        public static void Remove(string key)
        {
            RemoveKey(key);
            _memoryCache.Remove(key);
        }

        public static IEnumerable<(string Key, object Value)> GetAll()
        {
            var cacheList = new List<(string Key, object Value)>();
            var currentKeys = GetKeys();
            var notFoundKeys = new List<string>();
            foreach (var key in currentKeys)
            {
                if (TryGetValue(key, out object value))
                    cacheList.Add((key, value));
                else
                    notFoundKeys.Add(key);
            }
            RemoveKeys(notFoundKeys);
            return cacheList;
        }

        internal static List<string> GetKeys()
        {
            if (_memoryCache.TryGetValue(ContentKey, out object objVal))
            {
                return (List<string>)objVal;
            }
            return new List<string>();
        }

        internal static void UpdateKeys(List<string> keys)
        {
            using (var item = _memoryCache.CreateEntry(ContentKey))
            {
                item.Value = keys;
            }
        }

        internal static void RemoveKey(string key)
        {
            var currentKeys = GetKeys();
            currentKeys.Remove(key);
            UpdateKeys(currentKeys);
        }

        internal static void RemoveKeys(List<string> keys)
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

        internal static void AddKey(string key)
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