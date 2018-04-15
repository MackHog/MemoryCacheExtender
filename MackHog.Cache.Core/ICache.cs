using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace MackHog.Cache.Core
{
    public interface ICache
    {
        ICacheEntry CreateEntry(string key);
        IEnumerable<(string Key, object Value)> GetAll();
        List<string> GetKeys();
        void Remove(string key);
        bool TryGetValue(string key, out object value);
    }
}
