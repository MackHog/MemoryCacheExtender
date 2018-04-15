﻿using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Linq;

namespace MackHog.Cache.Core
{
    internal class CacheKeyManager
    {
        private readonly IMemoryCache _memoryCache;
        internal CacheKeyManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        internal List<string> GetKeys()
        {
            if (_memoryCache.TryGetValue(CacheManager.ContentKey, out object objVal))
            {
                return (List<string>)objVal;
            }
            return new List<string>();
        }

        internal void UpdateKeys(List<string> keys)
        {
            using (var item = _memoryCache.CreateEntry(CacheManager.ContentKey))
            {
                item.Value = keys;
            }
        }

        internal void RemoveKey(string key)
        {
            var currentKeys = GetKeys();
            currentKeys.Remove(key);
            UpdateKeys(currentKeys);
        }

        internal void RemoveKeys(List<string> keys)
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

        public void AddKey(string key)
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