using System;
using System.Collections.Generic;
using System.Text;

namespace MackHog.Cache.Core
{
    public class CacheEntity
    {
        public readonly string Key;
        public readonly object Value;
        public readonly DateTimeOffset? AbsoluteExpiration;
        public readonly TimeSpan? AbsoluteExpirationRelativeToNow;
        public readonly TimeSpan? SlidingExpiration;
        public readonly long? Size;

        public CacheEntity(
            string key, 
            object value, 
            DateTimeOffset? absoluteExpiration = null,
            TimeSpan? absoluteExpirationRelativeToNow = null,
            TimeSpan? slidingExpiration = null,
            long? size = null)
        {
            Key = key;
            Value = value;
            AbsoluteExpiration = absoluteExpiration;
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
            SlidingExpiration = slidingExpiration;
            Size = size;
        }
    }
}
