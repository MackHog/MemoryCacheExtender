using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace MackHog.Cache.Core
{
    public interface ICache
    {
        void Add(CacheEntity cacheEntity);
        void AddMany(IEnumerable<CacheEntity> entries);
        IEnumerable<(string Key, object Value)> GetAll();
        void Remove(string key);
        bool TryGetValue(string key, out object value);
    }
}
