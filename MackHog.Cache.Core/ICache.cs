using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace MackHog.Cache.Core
{
    public interface ICache
    {
        void Add(string key, object value, DateTimeOffset? absoluteExpiration = null, TimeSpan? slidingExpiration = null);
        void AddMany(IEnumerable<(string Key, object Value, DateTimeOffset? AbsoluteExpiration, TimeSpan? SlidingExpiration)> entries);
        IEnumerable<(string Key, object Value)> GetAll();
        void Remove(string key);
        bool TryGetValue(string key, out object value);
    }
}
