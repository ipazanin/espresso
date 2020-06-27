using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace Espresso.Application.Utility
{
    public static class MemoryCacheUtility
    {
        public static void ClearMemoryCache(IMemoryCache memoryCache, IEnumerable<object> keys)
        {
            foreach (var key in keys)
            {
                memoryCache.Remove(key);
            }
        }
    }
}
