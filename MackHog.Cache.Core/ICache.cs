using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace MackHog.Cache.Core
{
    public interface ICache
    {
        ICacheEntry CreateEntry(string key);
        IEnumerable<(string Key, object Value)> GetAll();
        IEnumerable<string> GetKeys();
        void Remove(string key);
        bool TryGetValue(string key, out object value);
    }
}