using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace MackHog.Cache.Core
{
    public interface ICache
    {
        string CacheContentKey { get; }
        ICacheEntry CreateEntry(string key);
        IEnumerable<(string Key, object Value)> GetAll();
        IEnumerable<string> GetKeys();
        void Remove(string key);
        bool TryGetValue(string key, out object value);
    }
}
